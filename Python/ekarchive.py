import tkinter as tk
from tkinter import ttk, messagebox
from tkcalendar import Calendar
import requests
import datetime

def fetch_data():
    selected_date = calendar.get_date()
    try:
        date = datetime.datetime.strptime(selected_date, "%d/%m/%Y")
        next_date = date + datetime.timedelta(days=1)
        formatted_date = date.strftime("%Y-%m-%d")
        formatted_next_date = next_date.strftime("%Y-%m-%d")
        
        url = f"https://api.raporty.pse.pl/api/pdgsz?$filter=udtczas ge '{formatted_date}' and udtczas lt '{formatted_next_date}'"
        
        response = requests.get(url)
        response.raise_for_status()
        data = response.json()
        
        data = data.get("value", [])
        
        for row in tree.get_children():
            tree.delete(row)
        
        znacznik_map = {
            0: "ZALECANE UŻYTKOWANIE",
            1: "NORMALNE UŻYTKOWANIE",
            2: "ZALECANE OSZCZĘDZANIE",
            3: "WYMAGANE OGRANICZANIE"
        }
        
        znacznik_colors = {
            0: "#226B11",  # Zielony ciemny
            1: "#98C21D",  # Zielony jasny
            2: "#F2C433",  # Żółty
            3: "#E42313"   # Czerwony
        }
        
        business_date_value = "Brak danych"
        source_datetime_value = "Brak danych"
        
        for item in data:
            udtczas = item.get("udtczas", "Brak danych")
            hour = udtczas.split(" ")[1] if udtczas != "Brak danych" else "Brak danych"
            znacznik = item.get("znacznik", "Brak danych")
            znacznik_text = znacznik_map.get(znacznik, "Nieznany znacznik")
            color = znacznik_colors.get(znacznik, "#FFFFFF")
            business_date = item.get("business_date", "Brak danych")
            source_datetime = item.get("source_datetime", "Brak danych")
            
            if source_datetime != "Brak danych":
                source_datetime = source_datetime.split(".")[0]
            
            if business_date != "Brak danych" and business_date_value == "Brak danych":
                business_date_value = business_date
            if source_datetime != "Brak danych" and source_datetime_value == "Brak danych":
                source_datetime_value = source_datetime
            
            tree.insert("", "end", values=(hour, znacznik_text), tags=(str(znacznik),))
        
        business_date_label.config(text=f"Doba handlowa: {business_date_value}")
        source_datetime_label.config(text=f"Data publikacji: {source_datetime_value}")
    
    except Exception as e:
        messagebox.showerror("Błąd", f"Nie udało się pobrać danych: {e}")

root = tk.Tk()
root.title("archiwum EK PSE")

frame = tk.Frame(root)
frame.pack(pady=10)
calendar_label = tk.Label(frame, text="Wybierz datę:")
calendar_label.pack(side=tk.LEFT, padx=5)

calendar = Calendar(frame, date_pattern="dd/MM/yyyy", locale="pl_PL")
calendar.pack(side=tk.LEFT, padx=5)

labels_frame = tk.Frame(root)
labels_frame.pack(pady=10)

business_date_label = tk.Label(labels_frame, text="Doba handlowa: Brak danych", font=("Arial", 12, "bold"))
business_date_label.pack(anchor="w", pady=5)

source_datetime_label = tk.Label(labels_frame, text="Data publikacji: Brak danych", font=("Arial", 12, "bold"))
source_datetime_label.pack(anchor="w", pady=5)

fetch_button = tk.Button(root, text="Pobierz dane", command=fetch_data)
fetch_button.pack(pady=10)

table_frame = tk.Frame(root)
table_frame.pack(fill=tk.BOTH, expand=True, padx=10, pady=10)

scrollbar = ttk.Scrollbar(table_frame, orient="vertical")
scrollbar.pack(side=tk.RIGHT, fill=tk.Y)

columns = ("hour", "alert")
tree = ttk.Treeview(table_frame, columns=columns, show="headings", yscrollcommand=scrollbar.set)
tree.heading("hour", text="Godzina")
tree.heading("alert", text="Alert")
tree.pack(fill=tk.BOTH, expand=True)

scrollbar.config(command=tree.yview)

tree.column("hour", width=100, anchor="center")
tree.column("alert", width=200, anchor="center")

style = ttk.Style()
style.map(
    "Treeview", 
    foreground=[("selected", "white")], 
    background=[("selected", "#0078D7")]
)

tree.tag_configure("0", background="#226B11", foreground="white")  # Zielony ciemny
tree.tag_configure("1", background="#98C21D", foreground="black")  # Zielony jasny
tree.tag_configure("2", background="#F2C433", foreground="black")  # Żółty
tree.tag_configure("3", background="#E42313", foreground="white")  # Czerwony

root.mainloop()
