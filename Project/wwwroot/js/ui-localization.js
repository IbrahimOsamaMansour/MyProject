(function () {
    "use strict";

    const translations = {
        "Language": "اللغة", "English": "الإنجليزية", "العربية": "العربية",
        "General": "عام", "Dashboard": "لوحة التحكم", "Setting": "الإعدادات",
        "Category": "الفئة", "Categories": "الفئات", "Company": "الشركة", "Companies": "الشركات",
        "SubCategory": "الفئة الفرعية", "Sub-Categories": "الفئات الفرعية", "SubCategories": "الفئات الفرعية",
        "Purchasing": "المشتريات", "Purchesing": "المشتريات", "Stock": "المخزون", "Products": "المنتجات",
        "Product": "المنتج", "Sales": "المبيعات", "Invoices": "الفواتير", "Invoice": "فاتورة",
        "Expenses": "المصروفات", "Item Expenses": "بنود المصروفات", "HR System": "الموارد البشرية",
        "Employee": "الموظف", "Workers": "العاملون", "Attendance": "الحضور", "Salary": "الراتب",
        "Welcome,": "مرحبًا،", "Welcome": "مرحبًا", "Log Out": "تسجيل الخروج", "logout": "تسجيل الخروج",
        "Login": "تسجيل الدخول", "Log IN": "تسجيل الدخول", "LOGIN": "تسجيل الدخول", "Register here": "سجّل هنا",
        "CREATE ACCOUNT": "إنشاء حساب", "Create New": "إنشاء جديد", "Create": "إنشاء", "Edit": "تعديل",
        "Delete": "حذف", "Details": "التفاصيل", "Detail": "التفاصيل", "Submit": "حفظ", "Reset": "إعادة تعيين",
        "Back to List": "العودة إلى القائمة", "Back to Detail": "العودة إلى التفاصيل", "Back to detailes": "العودة إلى التفاصيل",
        "Back to Attendance": "العودة إلى الحضور", "Back to Categories": "العودة إلى الفئات", "Back to Category": "العودة إلى الفئة",
        "Back to Products": "العودة إلى المنتجات", "Back to product": "العودة إلى المنتج", "Back to Purchasing": "العودة إلى المشتريات",
        "Back to Workers": "العودة إلى العاملين", "Back to Salaries": "العودة إلى الرواتب", "Back to Salary": "العودة إلى الراتب",
        "Add": "إضافة", "Add Bill": "إضافة فاتورة", "Add Category": "إضافة فئة", "Add Company": "إضافة شركة",
        "Add Employee": "إضافة موظف", "Add Expense": "إضافة مصروف", "Add Expenses": "إضافة مصروفات",
        "Add Item Expenses": "إضافة بند مصروفات", "Add Product": "إضافة منتج", "Add Purchesing": "إضافة مشتريات",
        "Add Quantity": "إضافة كمية", "Add Quantity to Product": "إضافة كمية إلى المنتج", "Add Phone": "إضافة هاتف",
        "Add Salary": "إضافة راتب", "Add SubCategory": "إضافة فئة فرعية", "Confirm Selling": "تأكيد البيع",
        "Search:": "بحث:", "search": "بحث", "Search Table Record:": "البحث في السجلات:",
        "Item to filter..": "عنصر للتصفية..", "Filter Category:": "تصفية الفئات:", "Filter Item Expenses:": "تصفية بنود المصروفات:",
        "Date": "التاريخ", "Date: @Model.Date.ToShortDateString()": "التاريخ: @Model.Date.ToShortDateString()",
        "Name": "الاسم", "Name Model": "اسم الموديل", "Model Name": "اسم الموديل", "Description": "الوصف", "Note": "ملاحظة",
        "Number": "العدد", "Quantity": "الكمية", "Qty": "الكمية", "Unit": "الوحدة", "Unit Cost": "تكلفة الوحدة",
        "Unit Price": "سعر الوحدة", "unit price": "سعر الوحدة", "Total": "الإجمالي", "Total Price": "السعر الإجمالي",
        "Total Price:": "السعر الإجمالي:", "Value": "القيمة", "Amount": "المبلغ", "Amount Due @Model.Date.ToShortDateString()": "المبلغ المستحق @Model.Date.ToShortDateString()",
        "Client name": "اسم العميل", "Client Name": "اسم العميل", "Client Number": "رقم العميل", "client phone": "هاتف العميل",
        "Phone": "الهاتف", "Person Contact": "بيانات التواصل", "Email": "البريد الإلكتروني", "Bill Number": "رقم الفاتورة",
        "Bill Information": "بيانات الفاتورة", "Bill Info": "بيانات الفاتورة", "Bill": "الفاتورة", "Billing": "الفواتير",
        "Booking": "الحجز", "Under Payment": "غير مكتملة السداد", "Bill is Sold": "تم بيع الفاتورة", "Bill State": "حالة الفاتورة",
        "Payed Amount": "المبلغ المدفوع", "Payed Quantity": "المبلغ المدفوع", "payed state": "حالة السداد",
        "The remaining quantity": "المبلغ المتبقي", "The remaining quantity:": "المبلغ المتبقي:", "Payment Due:": "موعد السداد:",
        "Payed quantities": "المدفوعات", "Recent Customer Payments": "أحدث مدفوعات العملاء", "Recent Expenses": "أحدث المصروفات",
        "Recent Sales": "أحدث المبيعات", "Customer": "العميل", "Product in Bill": "المنتجات في الفاتورة",
        "Product in Bill": "المنتجات في الفاتورة", "Products In stock": "المنتجات في المخزون", "Current Stock": "المخزون الحالي",
        "Stock Movement by Unit": "حركة المخزون حسب الوحدة", "Purchased / Received": "المشتراة / الواردة",
        "Sold / Out": "المباعة / المنصرفة", "Confirmed sold products": "المنتجات المباعة والمؤكدة",
        "Cash received from customers": "المبالغ المحصلة من العملاء", "Recorded purchase cost, not supplier payments": "تكلفة المشتريات المسجلة، وليست مدفوعات الموردين",
        "Sales Revenue (All Time)": "إيرادات المبيعات (منذ البداية)", "Customer Payments Collected (All Time)": "تحصيلات العملاء (منذ البداية)",
        "Purchase Value Recorded (All Time)": "قيمة المشتريات المسجلة (منذ البداية)", "Estimated Net Profit (All Time)": "صافي الربح التقديري (منذ البداية)",
        "Cost of Goods Sold (All Time)": "تكلفة البضاعة المباعة (منذ البداية)", "Gross Profit (All Time)": "إجمالي الربح (منذ البداية)",
        "Expenses Recorded (All Time)": "المصروفات المسجلة (منذ البداية)", "Payroll Recorded (All Time)": "الرواتب المسجلة (منذ البداية)",
        "Stock Value at Known Cost": "قيمة المخزون بالتكلفة المعروفة", "Accounting Notes": "ملاحظات محاسبية",
        "No confirmed sales yet.": "لا توجد مبيعات مؤكدة حتى الآن.", "No customer payments recorded.": "لا توجد مدفوعات مسجلة من العملاء.",
        "No expenses recorded.": "لا توجد مصروفات مسجلة.", "No stock movement recorded.": "لا توجد حركة مخزون مسجلة.",
        "Please choose product": "من فضلك اختر المنتج", "-- choose product --": "-- اختر المنتج --", "-- choose company --": "-- اختر الشركة --",
        "-- choose chategory --": "-- اختر الفئة --", "-- choose subchategory --": "-- اختر الفئة الفرعية --", "-- choose unite --": "-- اختر الوحدة --",
        "there aren't products": "لا توجد منتجات", "No Category": "لا توجد فئات", "No Company": "لا توجد شركات", "No SubCategory": "لا توجد فئات فرعية",
        "No Unite quantity": "لا توجد وحدة كمية", "Choose one": "اختر واحدًا", "Print": "طباعة", "Print / Save as PDF": "طباعة / حفظ PDF",
        "Save Edit": "حفظ التعديل", "Delete Purchasing": "حذف المشتريات", "Delete Product": "حذف المنتج", "Delete Company": "حذف الشركة",
        "Delete Salary": "حذف الراتب", "Create New Category": "إنشاء فئة جديدة", "Category Details": "تفاصيل الفئة",
        "Company Info": "بيانات الشركة", "Company Name": "اسم الشركة", "Details of Purchasing": "تفاصيل المشتريات",
        "Form Design": "تصميم النموذج", "Step 1": "الخطوة 1", "Step 2": "الخطوة 2", "Step 3": "الخطوة 3",
        "Date of purchasing": "تاريخ الشراء", "Quantity of purchasing": "كمية المشتريات", "Data of Product": "بيانات المنتج",
        "Data of prchasing": "بيانات المشتريات", "Invoice.": "الفاتورة.", "to Ceramic trade": "إلى تجارة السيراميك",
        "From": "من", "To": "إلى", "Sell Date": "تاريخ البيع", "Book Date": "تاريخ الحجز", "Expire Date": "تاريخ الانتهاء",
        "Expired Date": "تاريخ الانتهاء", "Date Expired": "تاريخ الانتهاء", "Description": "الوصف", "Details of Purchasing": "تفاصيل المشتريات",
        "Sorry! there is no Product": "عذرًا، لا توجد منتجات", "Sorry! there is no Quantity": "عذرًا، لا توجد كميات",
        "Sorry! there is no payed Quantities for this bill": "عذرًا، لا توجد مدفوعات لهذه الفاتورة",
        "Sorry ,there is no payed Quantity yet !": "عذرًا، لا توجد مدفوعات حتى الآن!", "Sorry there is No Product in Invoice": "عذرًا، لا توجد منتجات في الفاتورة",
        "Sorry there is No data here": "عذرًا، لا توجد بيانات هنا", "Sorry! there is no Product": "عذرًا، لا توجد منتجات",
        "Are you sure you want to delete this Bill?": "هل أنت متأكد أنك تريد حذف هذه الفاتورة؟",
        "Are you sure you want to delete prodct ?": "هل أنت متأكد أنك تريد حذف المنتج؟", "Are you sure you want to delete this?": "هل أنت متأكد أنك تريد الحذف؟",
        "Edit Company": "تعديل الشركة", "Edit Product": "تعديل المنتج", "Edit Purchasing": "تعديل المشتريات", "Edit Category": "تعديل الفئة",
        "Create New Item Expenses": "إنشاء بند مصروفات جديد", "Item Expenses Name :": "اسم بند المصروفات:", "Expense Item": "بند المصروفات",
        "Total Price:": "السعر الإجمالي:", "Paid Amount": "المبلغ المدفوع", "Submit Payment": "تسجيل دفعة", "Generate PDF": "إنشاء PDF",
        "Privacy": "الخصوصية", "Home": "الرئيسية", "Settings": "الإعدادات", "Help": "المساعدة", "Profile": "الملف الشخصي",
        "Showing 1 to 10 of 57 entries": "عرض 1 إلى 10 من 57 سجلًا", "Next": "التالي", "Previous": "السابق",
        "Showing": "عرض", "entries": "سجلات", "No data available in table": "لا توجد بيانات في الجدول",
        "Remember Me": "تذكرني", "Your Name": "اسمك", "Password": "كلمة المرور", "Confirm Password": "تأكيد كلمة المرور",
        "Your Address": "عنوانك", "Login here": "سجّل الدخول هنا", "Registration": "إنشاء حساب", "Add new payed Quanity": "إضافة دفعة جديدة",
        "Add new payed Quanit": "إضافة دفعة جديدة", "Add new payed Quantity": "إضافة دفعة جديدة", "Bill sell": "بيع الفاتورة",
        "Date of Payed": "تاريخ الدفع", "payment": "الدفع", "Unit Quantity": "وحدة القياس", "Value Quantity": "الكمية",
        "Category Name": "اسم الفئة", "Category Name :": "اسم الفئة:", "Category:": "الفئة:", "Companys": "الشركات",
        "Categorys": "الفئات", "Item Expencess": "بنود المصروفات", "Item Expnses Name": "اسم بند المصروفات",
        "Attendance OF Worker": "حضور العامل", "Employee Attendance": "حضور الموظف", "Employee Salary": "راتب الموظف",
        "All Attende": "كل الحضور", "ADD Attend": "إضافة حضور", "Add Attendance": "إضافة حضور", "Edit Form": "تعديل النموذج",
        "Insert Purchasing is Done Successfull": "تم تسجيل المشتريات بنجاح", "Delete": "حذف", "Reset": "إعادة تعيين",
        "All BILL": "كل الفواتير", "All SubCategoties": "كل الفئات الفرعية", "--Please Select Category--": "-- من فضلك اختر الفئة --",
        "Add Quanttiy": "إضافة كمية", "Add To Product": "إضافة إلى المنتج", "AddPhone": "إضافة هاتف", "DeletePhone": "حذف الهاتف",
        "Back bill detailes": "العودة إلى تفاصيل الفاتورة", "Back to bill detailes": "العودة إلى تفاصيل الفاتورة",
        "Back to Item Expenses List": "العودة إلى قائمة بنود المصروفات", "Back to emplyee Attendance": "العودة إلى حضور الموظف",
        "Be noted that the quantity will add in quantity of product": "تنبيه: ستُضاف هذه الكمية إلى كمية المنتج في المخزون",
        "Be noted that the quantity will delete from Reserved quantity in product": "تنبيه: ستُخصم هذه الكمية من الكمية المحجوزة للمنتج",
        "Bill#": "رقم الفاتورة", "Coming Soon": "قريبًا", "Copy": "نسخ", "CSV": "CSV", "Excel": "Excel", "PDF": "PDF",
        "Data of Produt": "بيانات المنتج", "date sell": "تاريخ البيع", "Detail Company": "تفاصيل الشركة", "Edit phone": "تعديل الهاتف",
        "Edit Product is Done Successfull": "تم تعديل المنتج بنجاح", "edit Purchaching": "تعديل المشتريات",
        "Edit Purchasing is Done Successfull": "تم تعديل المشتريات بنجاح", "editQuantity": "تعديل الكمية",
        "Finshed": "مكتمل", "Go!": "تنفيذ", "Gross profit − recorded expenses − recorded payroll": "إجمالي الربح − المصروفات المسجلة − الرواتب المسجلة",
        "Have already an account ?": "لديك حساب بالفعل؟", "if you don't have account !": "إذا لم يكن لديك حساب!",
        "Insert Purchasing in Product is Done Successfull": "تم إدخال المشتريات إلى المنتجات بنجاح", "Line Total": "إجمالي السطر",
        "No data available in table": "لا توجد بيانات في الجدول", "Notes:": "ملاحظات:", "Order ID:": "رقم الطلب:",
        "payed Qunatity in Bill": "المبلغ المدفوع في الفاتورة", "payed Total Price": "إجمالي المبلغ المدفوع", "payed value": "المبلغ المدفوع",
        "Payment Methods:": "طرق الدفع:", "Phone #:": "رقم الهاتف:", "Phone Company": "هاتف الشركة", "Phone Companye": "هاتف الشركة",
        "Phone: @Model.Client_phone": "الهاتف: @Model.Client_phone", "Plain Page": "صفحة بسيطة", "Pricing Tables": "جداول الأسعار",
        "Product is already sold before": "تم بيع هذا المنتج من قبل", "Product is booked": "المنتج محجوز", "product is sold": "تم بيع المنتج",
        "product Quantity": "كمية المنتج", "Produuct in Bill": "المنتجات في الفاتورة", "produuct Model Name": "اسم موديل المنتج",
        "Quantity of Product": "كمية المنتج", "Quantity reserved": "الكمية المحجوزة", "Quntity": "الكمية", "Reserved Quantity": "الكمية المحجوزة",
        "resive bill": "استلام الفاتورة", "Sales revenue − cost of sold stock": "إيرادات المبيعات − تكلفة البضاعة المباعة",
        "Settings 1": "الإعدادات 1", "Settings 2": "الإعدادات 2", "Show": "عرض", "Sold": "مباع",
        "Sorry Delete Attendance is Fail!": "عذرًا، فشل حذف الحضور!", "Sorry Delete Purchasing is Fail!": "عذرًا، فشل حذف المشتريات!",
        "Sorry Delete Quantity is Fail!": "عذرًا، فشل حذف الكمية!", "Sorry Edit Product is Fail!": "عذرًا، فشل تعديل المنتج!",
        "Sorry Edit Purchasing is Fail!": "عذرًا، فشل تعديل المشتريات!", "Sorry Insert Purchasing is Fail!": "عذرًا، فشل تسجيل المشتريات!",
        "Sorry thete is No Phone For this Company !": "عذرًا، لا يوجد هاتف مسجل لهذه الشركة!",
        "Sorry thete is No Sub Categories For this Category !": "عذرًا، لا توجد فئات فرعية لهذه الفئة!",
        "The client recevied All product in this bill": "استلم العميل جميع المنتجات في هذه الفاتورة",
        "total payed": "إجمالي المدفوع", "Total payed:": "إجمالي المدفوع:", "Unit Quantity": "وحدة القياس",
        "Use this page to detail your site's privacy policy.": "استخدم هذه الصفحة لعرض سياسة الخصوصية الخاصة بموقعك.",
        "Worker": "العامل", "No Category": "لا توجد فئات", "Category ID": "رقم الفئة",
        "The category Name field is required.": "حقل اسم الفئة مطلوب.", "Expense Value Is Required": "قيمة المصروف مطلوبة",
        "Date is Required": "التاريخ مطلوب", "invalid Phone": "رقم الهاتف غير صالح", "UserName and Password invalied": "اسم المستخدم أو كلمة المرور غير صحيحة",
        "Please choose product": "من فضلك اختر منتجًا", "Please choose unit quantity": "من فضلك اختر وحدة القياس",
        "Enter a selling price greater than zero.": "أدخل سعر بيع أكبر من صفر.",
        "A purchase cost is not recorded for this product and unit. Add the unit cost in Stock before selling.": "تكلفة الشراء غير مسجلة لهذا المنتج والوحدة. أضف تكلفة الوحدة في المخزون قبل البيع.",
        "Please select a category.": "من فضلك اختر فئة.", "Please choose Unit": "من فضلك اختر الوحدة", "Please add value": "من فضلك أدخل قيمة",
        "Date Must Be Before Today": "يجب أن يكون التاريخ قبل اليوم", "Updated Successfully": "تم التحديث بنجاح", "Deleted Successfully": "تم الحذف بنجاح",
        "All BILL": "كل الفواتير", "Add quantity to product  is Done Successfull": "تمت إضافة الكمية إلى المنتج بنجاح",
        "Add Quanttiy": "إضافة كمية", "Add To Product": "إضافة إلى المنتج", "All SubCategoties": "كل الفئات الفرعية",
        "Are you sure you want to  Delete this  Category ?": "هل أنت متأكد أنك تريد حذف هذه الفئة؟",
        "Are you sure you want to delete this Attendance?": "هل أنت متأكد أنك تريد حذف سجل الحضور؟",
        "Are you sure you want to delete this item Expenses ?": "هل أنت متأكد أنك تريد حذف بند المصروفات؟",
        "Are you sure you want to delete this Payed quantity?": "هل أنت متأكد أنك تريد حذف هذه الدفعة؟",
        "Are you sure you want to delete this Salary ?": "هل أنت متأكد أنك تريد حذف هذا الراتب؟",
        "Are you sure you want to delete this Worker ?": "هل أنت متأكد أنك تريد حذف هذا العامل؟",
        "Back to Item Expenses List": "العودة إلى قائمة بنود المصروفات", "Back to bill detailes": "العودة إلى تفاصيل الفاتورة",
        "Back to emplyee Attendance": "العودة إلى حضور الموظف", "Cairo Governorate": "محافظة القاهرة", "Ceramica Cleopatra.": "سيراميكا كليوباترا.",
        "Customer payments are recorded as cash received.": "تُسجل مدفوعات العملاء باعتبارها مبالغ نقدية محصلة.",
        "Data of Produt": "بيانات المنتج", "Delete Product In Bill": "حذف المنتج من الفاتورة", "Detail Company": "تفاصيل الشركة",
        "Development Mode": "وضع التطوير", "Edit Product is Done Successfull": "تم تعديل المنتج بنجاح",
        "Edit Purchasing is Done Successfull": "تم تعديل المشتريات بنجاح", "Finshed": "مكتمل", "go to Bills": "الانتقال إلى الفواتير",
        "Have already an account ?": "لديك حساب بالفعل؟", "if you don't have account !": "إذا لم يكن لديك حساب!",
        "Insert Purchasing in Product is Done Successfull": "تم إدخال المشتريات إلى المنتجات بنجاح", "No data available in table": "لا توجد بيانات بالجدول",
        "Please Delete All Product  in this Bill": "من فضلك احذف كل المنتجات من هذه الفاتورة أولًا",
        "Please Delete All Payed quantity  in this Bill": "من فضلك احذف كل الدفعات من هذه الفاتورة أولًا",
        "Please Delete All Quantity in this Product": "من فضلك احذف كل كميات هذا المنتج أولًا",
        "Please Delete this product from all bills": "من فضلك احذف هذا المنتج من كل الفواتير أولًا",
        "Please add Company": "من فضلك أضف شركة", "Please add Category": "من فضلك أضف فئة", "Choose unite please": "من فضلك اختر الوحدة",
        "Please choose Unit": "من فضلك اختر الوحدة", "Please add value": "من فضلك أدخل قيمة", "Value invaled": "القيمة غير صالحة",
        "You can't Add Quuantity you already add two  Quantity Before": "لا يمكن إضافة كمية أخرى؛ لقد أضفت الكمية المسموح بها مسبقًا",
        "Sorry! Add Quantity is failed": "عذرًا، فشلت إضافة الكمية", "Delete Quantity is success": "تم حذف الكمية بنجاح",
        "Delete Quantity is Faild": "فشل حذف الكمية", "Product is Add Successfully": "تمت إضافة المنتج بنجاح",
        "Product is added in bill successfully": "تمت إضافة المنتج إلى الفاتورة بنجاح",
        "You can't sold bill has no products!": "لا يمكن تأكيد بيع فاتورة لا تحتوي على منتجات!",
        "change state bill to sell": "تم تغيير حالة الفاتورة إلى مباعة", "update reserved quantity of product is failed": "فشل تحديث الكمية المحجوزة للمنتج",
        "update Real quantity of product is failed please try Again!": "فشل تحديث الكمية الفعلية للمنتج، حاول مرة أخرى!",
        "change state bill to sell is failed please try again": "فشل تغيير حالة الفاتورة، حاول مرة أخرى",
        "please choose unit": "من فضلك اختر الوحدة", "please delete Purchasing first !": "من فضلك احذف المشتريات أولًا!",
        "please delete Product first !": "من فضلك احذف المنتج أولًا!", "please delete sub categories first !": "من فضلك احذف الفئات الفرعية أولًا!",
        "Subcategory is not exist to delete it": "الفئة الفرعية غير موجودة لحذفها", "Deleted subcategory is Done Successfully": "تم حذف الفئة الفرعية بنجاح",
        "Please Delete All Employee Attendance  ": "من فضلك احذف سجلات حضور الموظف أولًا", "Please Delete All Employee Salary  ": "من فضلك احذف سجلات راتب الموظف أولًا",
        "Edit Quantity is Done": "تم تعديل الكمية", "Item Expense is a must And Dont Forget Date": "بند المصروف والتاريخ مطلوبان",
        "Log-in": "تسجيل الدخول", "Register": "تسجيل", "Bill_Num": "رقم الفاتورة", "Client_Name": "اسم العميل",
        "Client_phone": "هاتف العميل", "Exp_Date": "تاريخ الانتهاء", "Total_Price": "السعر الإجمالي", "Bill_State": "حالة الفاتورة",
        "Date_Payed": "تاريخ الدفع", "Expense_Date": "تاريخ المصروف", "Item_Ex_ID": "بند المصروفات", "UnitQuantity": "وحدة القياس",
        "ValueQuantity": "الكمية", "ModelName": "اسم الموديل", "PersonContact": "بيانات التواصل", "SSN": "الرقم القومي",
        "Phone Company": "هاتف الشركة", "Phone Companye": "هاتف الشركة", "From": "من", "To": "إلى",
        "About:": "حول:", "Info": "معلومات", "No data available in table": "لا توجد بيانات في الجدول",
        "The field is required.": "هذا الحقل مطلوب.", "This field is required.": "هذا الحقل مطلوب.",
        "Please enter a valid value.": "من فضلك أدخل قيمة صحيحة.", "The value is not valid.": "القيمة غير صالحة.",
        "The value 'NULL' is not valid for Suc_Categ_Id.": "اختيار الفئة الفرعية مطلوب.",
        "Add Bill": "إضافة فاتورة", "Create New Item Expenses": "إنشاء بند مصروفات جديد", "Create New Category": "إنشاء فئة جديدة",
        "Add new payed Quanity": "إضافة دفعة جديدة", "Add new payed Quanit": "إضافة دفعة جديدة", "Payed Amount": "المبلغ المدفوع",
        "Total paid:": "إجمالي المدفوع:", "Total Paid:": "إجمالي المدفوع:", "The remaining amount:": "المبلغ المتبقي:",
        "Current Stock": "المخزون الحالي", "No stock movement recorded.": "لا توجد حركة مخزون مسجلة.",
        "No confirmed sales yet.": "لا توجد مبيعات مؤكدة حتى الآن.", "No customer payments recorded.": "لا توجد مدفوعات مسجلة من العملاء.",
        "No expenses recorded.": "لا توجد مصروفات مسجلة.", "No SubCategory": "لا توجد فئات فرعية", "No Company": "لا توجد شركات",
        "No Category": "لا توجد فئات", "Please select a category.": "من فضلك اختر فئة.",
        "Please choose company": "من فضلك اختر الشركة", "Please choose category": "من فضلك اختر الفئة",
        "Please choose subcategory": "من فضلك اختر الفئة الفرعية", "Please choose unit": "من فضلك اختر الوحدة",
        "Create SubCategory": "إنشاء فئة فرعية", "Create Category": "إنشاء فئة", "Create Company": "إنشاء شركة",
        "Create Product": "إنشاء منتج", "Create Bill": "إنشاء فاتورة", "Add Purchesing": "إضافة مشتريات",
        "Name Model *": "اسم الموديل *", "Company *": "الشركة *", "Category: *": "الفئة: *", "Category: *": "الفئة: *",
        "Data of purchasing": "بيانات الشراء", "Quantity of Product": "كمية المنتج", "Quantity reserved": "الكمية المحجوزة",
        "Date of Payed": "تاريخ الدفع", "Date Book": "تاريخ الحجز", "Date:": "التاريخ:", "Amount Due": "المبلغ المستحق",
        "Payed Amount": "المبلغ المدفوع", "Exp Date:": "تاريخ الانتهاء:", "Invoice #": "رقم الفاتورة #",
        "Add new payed Quanity": "إضافة دفعة جديدة", "Add new payed Quanit": "إضافة دفعة جديدة",
        "Profile": "الملف الشخصي", "Help": "المساعدة", "Settings": "الإعدادات", "General": "عام",
        "Additional Pages": "صفحات إضافية", "Advanced": "متقدم", "Form Buttons": "أزرار النماذج", "Form Elements": "عناصر النماذج",
        "Form Upload": "رفع الملفات", "Form Validation": "التحقق من النماذج", "Form Wizard": "معالج النماذج",
        "Coming Soon": "قريبًا", "Contacts": "جهات الاتصال", "Extras": "إضافات", "E-commerce": "التجارة الإلكترونية",
        "Error.": "خطأ.", "About:": "حول:", "Request ID:": "معرّف الطلب:", "Development": "التطوير", "Development Mode": "وضع التطوير",
        "The development environment shouldn't be enabled for deployed applications.": "يجب عدم تفعيل بيئة التطوير في التطبيقات المنشورة.",
        "Search for...": "ابحث عن...", "No data available in table": "لا توجد بيانات في الجدول", "Go!": "تنفيذ",
        "Add new paid amount": "إضافة مبلغ مدفوع جديد", "Paid Amount": "المبلغ المدفوع", "Total paid": "إجمالي المدفوع",
        "Amount Due": "المبلغ المستحق", "Unit Price": "سعر الوحدة", "Line Total": "إجمالي السطر",
        "Step 3 description": "وصف الخطوة 3", "Data of Product": "بيانات المنتج", "Data of Produt": "بيانات المنتج",
        "Quantity of purchasing": "كمية الشراء", "Date of purchasing": "تاريخ الشراء", "Add Product": "إضافة منتج",
        "SubCategory:": "الفئة الفرعية:", "Item Expenses Name :": "اسم بند المصروفات:", "Filter Category:": "تصفية الفئات:",
        "Filter Item Expenses:": "تصفية بنود المصروفات:", "Category Name :": "اسم الفئة:", "Company Name": "اسم الشركة",
        "Employee Salary": "راتب الموظف", "Employee Attendance": "حضور الموظف", "All Attende": "كل سجلات الحضور",
        "ADD Attend": "إضافة حضور", "Add Attendance": "إضافة حضور", "Insert Purchasing is Done Successfull": "تم تسجيل المشتريات بنجاح",
        "Item Expense is a must And Dont Forget Date": "بند المصروف والتاريخ مطلوبان", "Updated Successfully": "تم التحديث بنجاح",
        "Expense Value Is Required": "قيمة المصروف مطلوبة", "Date is Required": "التاريخ مطلوب", "invalid Phone": "رقم الهاتف غير صالح",
        "Date Must Be Before Today": "يجب أن يكون التاريخ قبل اليوم", "Updated Successfully": "تم التحديث بنجاح",
        "Deleted Successfully": "تم الحذف بنجاح", "Added Sucessfully": "تمت الإضافة بنجاح",
        "Please choose unit quantity": "من فضلك اختر وحدة القياس", "Enter a unit price greater than zero.": "أدخل سعر وحدة أكبر من صفر.",
        "Enter a selling price greater than zero.": "أدخل سعر بيع أكبر من صفر.", "Please add value": "من فضلك أدخل قيمة",
        "Please add Company": "من فضلك أضف شركة", "Please add Category": "من فضلك أضف فئة", "Choose unite please": "من فضلك اختر الوحدة"
    };

    function translateNode(node) {
        const original = node.nodeValue;
        const trimmed = original.trim();
        if (!trimmed) return;
        let key = Object.prototype.hasOwnProperty.call(translations, trimmed) ? trimmed : null;
        if (!key) {
            key = Object.keys(translations)
                .filter(candidate => candidate.includes(" ") || /[:#?]$/.test(candidate))
                .sort((a, b) => b.length - a.length)
                .find(candidate => trimmed.startsWith(candidate) && /^[\s:#]/.test(trimmed.slice(candidate.length)));
        }
        if (!key) return;
        const start = original.indexOf(trimmed);
        node.nodeValue = original.slice(0, start) + translations[key] + trimmed.slice(key.length) + original.slice(start + trimmed.length);
    }

    function translateElement(element) {
        if (!element || element.nodeType !== Node.ELEMENT_NODE) return;
        if (element.matches("script, style, textarea, input[type='hidden']")) return;

        const walker = document.createTreeWalker(element, NodeFilter.SHOW_TEXT, {
            acceptNode(node) {
                const parent = node.parentElement;
                if (!parent || parent.closest("script, style, textarea, input[type='hidden']")) return NodeFilter.FILTER_REJECT;
                return NodeFilter.FILTER_ACCEPT;
            }
        });
        let node;
        while ((node = walker.nextNode())) translateNode(node);

        for (const attribute of ["placeholder", "title", "aria-label", "value"]) {
            const value = element.getAttribute(attribute);
            if (value && Object.prototype.hasOwnProperty.call(translations, value.trim()))
                element.setAttribute(attribute, translations[value.trim()]);
        }
    }

    function applyArabicTranslations() {
        if (document.documentElement.lang !== "ar") return;
        translateElement(document.body);
        const observer = new MutationObserver(records => {
            for (const record of records) {
                if (record.type === "characterData") translateNode(record.target);
                record.addedNodes.forEach(node => {
                    if (node.nodeType === Node.TEXT_NODE) translateNode(node);
                    else translateElement(node);
                });
            }
        });
        observer.observe(document.body, { childList: true, characterData: true, subtree: true });
    }

    if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", applyArabicTranslations);
    else applyArabicTranslations();
})();
