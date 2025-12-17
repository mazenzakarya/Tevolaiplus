# Tevolai API Documentation for Angular Frontend

## Base URL
```
Development: http://localhost:5174 or https://localhost:7034
Production: [Your Production URL]
```

## Authentication

### JWT Token
All protected endpoints require a JWT token in the Authorization header:
```
Authorization: Bearer <token>
```

### Register User
**POST** `/api/Auth/register`

**Request Body:**
```json
{
  "email": "user@example.com",
  "password": "Password123!",
  "userName": "johndoe",
  "firstName": "John",
  "lastName": "Doe"
}
```

**Response:** `200 OK`
```json
{
  "id": "user-id",
  "token": "jwt-token-string",
  "expiresIn": 3600,
  "userName": "johndoe",
  "email": "user@example.com"
}
```

**Error Response:** `400 Bad Request`
```json
{
  "message": "Error message"
}
```

### Login
**POST** `/api/Auth/login`

**Request Body:**
```json
{
  "email": "user@example.com",
  "password": "Password123!"
}
```

**Response:** `200 OK`
```json
{
  "token": "jwt-token-string"
}
```

**Error Response:** `400 Bad Request`
```json
{
  "message": "Invalid credentials"
}
```

---

## Customers API

### Get All Customers
**GET** `/api/Customer/GetAllCustomers`

**Headers:**
```
Authorization: Bearer <token>
```

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "fullName": "John Doe",
    "email": "john.doe@example.com",
    "phone": "+1-555-0101",
    "companyName": "Tech Solutions Inc.",
    "country": "USA",
    "notes": "Premium client",
    "createdAt": "2024-01-01T00:00:00Z"
  }
]
```

### Get Customer By ID
**GET** `/api/Customer/GetCustomerById/{id}`

**Example:** `/api/Customer/GetCustomerById/1`

**Response:** `200 OK`
```json
{
  "id": 1,
  "fullName": "John Doe",
  "email": "john.doe@example.com",
  "phone": "+1-555-0101",
  "companyName": "Tech Solutions Inc.",
  "country": "USA",
  "notes": "Premium client",
  "createdAt": "2024-01-01T00:00:00Z"
}
```

**Error Response:** `404 Not Found`
```json
{
  "error": "Customer not found"
}
```

### Add Customer
**POST** `/api/Customer/AddCustomer`

**Request Body:**
```json
{
  "id": 0,
  "fullName": "Jane Smith",
  "email": "jane@example.com",
  "phone": "+1-555-0102",
  "companyName": "Company Name",
  "country": "USA",
  "notes": "New client",
  "createdAt": "2024-01-01T00:00:00Z"
}
```

**Response:** `200 OK` (Empty body)

### Update Customer
**PUT** `/api/Customer/UpdateCustomer`

**Request Body:**
```json
{
  "id": 1,
  "fullName": "John Doe Updated",
  "email": "john.updated@example.com",
  "phone": "+1-555-0101",
  "companyName": "Tech Solutions Inc.",
  "country": "USA",
  "notes": "Updated notes",
  "createdAt": "2024-01-01T00:00:00Z"
}
```

**Response:** `200 OK` (Empty body)

### Delete Customer
**DELETE** `/api/Customer/DeleteCustomer/{id}`

**Example:** `/api/Customer/DeleteCustomer/1`

**Response:** `200 OK` (Empty body)

### Search Customers
**GET** `/api/Customer/SearchCustomers?query={searchTerm}`

**Example:** `/api/Customer/SearchCustomers?query=john`

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "fullName": "John Doe",
    ...
  }
]
```

---

## Expenses API

### Get All Expenses (Paginated)
**GET** `/api/Expense?pageNumper={page}&pageSize={size}`

**Example:** `/api/Expense?pageNumper=1&pageSize=10`

**Query Parameters:**
- `pageNumper` (int, default: 1) - Page number
- `pageSize` (int, default: 10) - Items per page

**Response:** `200 OK`
```json
{
  "items": [
    {
      "id": 1,
      "title": "Server Hosting",
      "amount": 150.00,
      "category": "Infrastructure",
      "date": "2024-01-01T00:00:00Z",
      "notes": "Monthly hosting fee",
      "isDeleted": false
    }
  ],
  "totalCount": 100,
  "pageNumber": 1,
  "pageSize": 10,
  "totalPages": 10
}
```

### Get Expense By ID
**GET** `/api/Expense/{id}`

**Example:** `/api/Expense/1`

**Response:** `200 OK`
```json
{
  "id": 1,
  "title": "Server Hosting",
  "amount": 150.00,
  "category": "Infrastructure",
  "date": "2024-01-01T00:00:00Z",
  "notes": "Monthly hosting fee",
  "isDeleted": false
}
```

### Add Expense
**POST** `/api/Expense`

**Request Body:**
```json
{
  "id": 0,
  "title": "Domain Renewal",
  "amount": 15.00,
  "category": "Domain",
  "date": "2024-01-01T00:00:00Z",
  "notes": "Annual renewal",
  "isDeleted": false
}
```

**Response:** `200 OK` (Empty body)

### Update Expense
**PUT** `/api/Expense`

**Request Body:**
```json
{
  "id": 1,
  "title": "Server Hosting Updated",
  "amount": 200.00,
  "category": "Infrastructure",
  "date": "2024-01-01T00:00:00Z",
  "notes": "Updated notes",
  "isDeleted": false
}
```

**Response:** `200 OK` (Empty body)

### Delete Expense
**DELETE** `/api/Expense/{id}`

**Example:** `/api/Expense/1`

**Response:** `200 OK` (Empty body)

### Get Total Expenses Amount
**GET** `/api/Expense/count`

**Response:** `200 OK`
```json
{
  "totalAmount": 1500.00
}
```

---

## Revenues API

### Get Total Revenues Amount
**GET** `/api/Revenue/TotalRevenues`

**Response:** `200 OK`
```json
{
  "totalAmount": 15000.00
}
```

### Add Revenue
**POST** `/api/Revenue/AddRevenue`

**Request Body:**
```json
{
  "id": 0,
  "customerId": 1,
  "serviceId": 1,
  "amount": 5000.00,
  "paymentDate": "2024-01-01T00:00:00Z",
  "paymentMethod": "Bank Transfer",
  "notes": "Initial payment"
}
```

**Response:** `200 OK` (Empty body)

### Get Revenue By ID
**GET** `/api/Revenue/GetRevenueById/{id}`

**Example:** `/api/Revenue/GetRevenueById/1`

**Response:** `200 OK`
```json
{
  "id": 1,
  "customerId": 1,
  "serviceId": 1,
  "amount": 5000.00,
  "paymentDate": "2024-01-01T00:00:00Z",
  "paymentMethod": "Bank Transfer",
  "notes": "Initial payment"
}
```

### Update Revenue
**PUT** `/api/Revenue/EditRevenue`

**Request Body:**
```json
{
  "id": 1,
  "customerId": 1,
  "serviceId": 1,
  "amount": 5500.00,
  "paymentDate": "2024-01-01T00:00:00Z",
  "paymentMethod": "Credit Card",
  "notes": "Updated payment"
}
```

**Response:** `200 OK` (Empty body)

### Delete Revenue
**DELETE** `/api/Revenue/DeleteRevenue/{id}`

**Example:** `/api/Revenue/DeleteRevenue/1`

**Response:** `200 OK` (Empty body)

---

## Contact Messages API

### Add Contact Message
**POST** `/api/ContactMessage/AddContactMessage`

**Request Body:**
```json
{
  "id": 0,
  "name": "John Doe",
  "email": "john@example.com",
  "message": "I'm interested in your services",
  "createdAt": "2024-01-01T00:00:00Z"
}
```

**Response:** `200 OK` (Empty body)

### Get Paged Contact Messages
**GET** `/api/ContactMessage/GetPagedContactMessages?page={page}&pageSize={size}`

**Example:** `/api/ContactMessage/GetPagedContactMessages?page=1&pageSize=10`

**Query Parameters:**
- `page` (int, default: 1) - Page number
- `pageSize` (int, default: 10) - Items per page

**Response:** `200 OK`
```json
{
  "items": [
    {
      "id": 1,
      "name": "John Doe",
      "email": "john@example.com",
      "message": "I'm interested in your services",
      "createdAt": "2024-01-01T00:00:00Z"
    }
  ],
  "totalCount": 50,
  "pageNumber": 1,
  "pageSize": 10,
  "totalPages": 5
}
```

### Get Contact Message By ID
**GET** `/api/ContactMessage/GetContactMessageById/{id}`

**Example:** `/api/ContactMessage/GetContactMessageById/1`

**Response:** `200 OK`
```json
{
  "id": 1,
  "name": "John Doe",
  "email": "john@example.com",
  "message": "I'm interested in your services",
  "createdAt": "2024-01-01T00:00:00Z"
}
```

### Delete Contact Message
**DELETE** `/api/ContactMessage/DeleteContactMessage/{id}`

**Example:** `/api/ContactMessage/DeleteContactMessage/1`

**Response:** `200 OK` (Empty body)

### Mark Contact Message as Read
**PUT** `/api/ContactMessage/MarkAsRead/{id}`

**Example:** `/api/ContactMessage/MarkAsRead/1`

**Response:** `200 OK` (Empty body)

### Get Contact Messages By Read Status
**GET** `/api/ContactMessage/GetContactMessagesByReadStatus?isRead={true|false}`

**Example:** `/api/ContactMessage/GetContactMessagesByReadStatus?isRead=false`

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "name": "John Doe",
    "email": "john@example.com",
    "message": "I'm interested in your services",
    "createdAt": "2024-01-01T00:00:00Z"
  }
]
```

---

## Services on Main API

### Get All Services on Main
**GET** `/api/ServicesOnMain`

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "title": "Website Development",
    "description": "Professional website development",
    "price": 5000.00,
    "imageUrl": "/images/services/website-dev.jpg",
    "category": "Development",
    "isActive": true,
    "createdAt": "2024-01-01T00:00:00Z",
    "isDeleted": false
  }
]
```

### Get Service on Main By ID
**GET** `/api/ServicesOnMain/{id}`

**Example:** `/api/ServicesOnMain/1`

**Response:** `200 OK`
```json
{
  "id": 1,
  "title": "Website Development",
  "description": "Professional website development",
  "price": 5000.00,
  "imageUrl": "/images/services/website-dev.jpg",
  "category": "Development",
  "isActive": true,
  "createdAt": "2024-01-01T00:00:00Z",
  "isDeleted": false
}
```

### Add Service on Main
**POST** `/api/ServicesOnMain`

**Request Body:**
```json
{
  "id": 0,
  "title": "New Service",
  "description": "Service description",
  "price": 3000.00,
  "imageUrl": "/images/services/new.jpg",
  "category": "Development",
  "isActive": true,
  "createdAt": "2024-01-01T00:00:00Z",
  "isDeleted": false
}
```

**Response:** `200 OK` (Empty body)

### Update Service on Main
**PUT** `/api/ServicesOnMain`

**Request Body:**
```json
{
  "id": 1,
  "title": "Updated Service",
  "description": "Updated description",
  "price": 3500.00,
  "imageUrl": "/images/services/updated.jpg",
  "category": "Development",
  "isActive": true,
  "createdAt": "2024-01-01T00:00:00Z",
  "isDeleted": false
}
```

**Response:** `200 OK` (Empty body)

### Delete Service on Main
**DELETE** `/api/ServicesOnMain/{id}`

**Example:** `/api/ServicesOnMain/1`

**Response:** `200 OK` (Empty body)

---

## Services on Dashboard API

### Get All Services on Dashboard
**GET** `/api/ServicesOnDashBoard/GetAllServicesOnDashBoard`

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "customerId": 1,
    "serviceName": "John's WordPress Site",
    "domain": "johndoe.com",
    "userName": "johndoe_admin",
    "cms": 0,
    "hostingServer": "Bluehost",
    "status": 0,
    "notes": "Production site",
    "createdAt": "2024-01-01T00:00:00Z",
    "isDeleted": false
  }
]
```

**CMS Enum Values:**
- `0` - WordPress
- `1` - DotNet
- `2` - Joomla
- `3` - Drupal
- `4` - Custom

**Status Enum Values:**
- `0` - Active
- `1` - Suspended
- `2` - InProgress

### Get Service on Dashboard By ID
**GET** `/api/ServicesOnDashBoard/GetServiceOnDashBoardById/{id}`

**Example:** `/api/ServicesOnDashBoard/GetServiceOnDashBoardById/1`

**Response:** `200 OK`
```json
{
  "id": 1,
  "customerId": 1,
  "serviceName": "John's WordPress Site",
  "domain": "johndoe.com",
  "userName": "johndoe_admin",
  "cms": 0,
  "hostingServer": "Bluehost",
  "status": 0,
  "notes": "Production site",
  "createdAt": "2024-01-01T00:00:00Z",
  "isDeleted": false
}
```

### Add Service on Dashboard
**POST** `/api/ServicesOnDashBoard/AddServiceOnDashBoard`

**Request Body:**
```json
{
  "id": 0,
  "customerId": 1,
  "serviceName": "New Service",
  "domain": "newsite.com",
  "userName": "admin_user",
  "cms": 0,
  "hostingServer": "AWS",
  "status": 2,
  "notes": "Under development",
  "createdAt": "2024-01-01T00:00:00Z",
  "isDeleted": false
}
```

**Response:** `200 OK` (Empty body)

### Update Service on Dashboard
**PUT** `/api/ServicesOnDashBoard/UpdateServiceOnDashBoard`

**Request Body:**
```json
{
  "id": 1,
  "customerId": 1,
  "serviceName": "Updated Service",
  "domain": "updated.com",
  "userName": "updated_user",
  "cms": 1,
  "hostingServer": "Azure",
  "status": 0,
  "notes": "Updated notes",
  "createdAt": "2024-01-01T00:00:00Z",
  "isDeleted": false
}
```

**Response:** `200 OK` (Empty body)

### Delete Service on Dashboard
**DELETE** `/api/ServicesOnDashBoard/DeleteServiceOnDashBoard/{id}`

**Example:** `/api/ServicesOnDashBoard/DeleteServiceOnDashBoard/1`

**Response:** `200 OK` (Empty body)

### Search Services By Name
**GET** `/api/ServicesOnDashBoard/SearchServicesByName/{serviceName}`

**Example:** `/api/ServicesOnDashBoard/SearchServicesByName/wordpress`

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "serviceName": "John's WordPress Site",
    ...
  }
]
```

### Filter Services By CMS
**GET** `/api/ServicesOnDashBoard/FilterServicesByCMS/{cms}`

**Example:** `/api/ServicesOnDashBoard/FilterServicesByCMS/0`

**CMS Values:** 0 (WordPress), 1 (DotNet), 2 (Joomla), 3 (Drupal), 4 (Custom)

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "cms": 0,
    ...
  }
]
```

### Filter Services By Status
**GET** `/api/ServicesOnDashBoard/FilterServicesByStatus/{status}`

**Example:** `/api/ServicesOnDashBoard/FilterServicesByStatus/0`

**Status Values:** 0 (Active), 1 (Suspended), 2 (InProgress)

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "status": 0,
    ...
  }
]
```

### Sort Services By Created At
**GET** `/api/ServicesOnDashBoard/SortServicesByCreatedAt/{ascending}`

**Example:** `/api/ServicesOnDashBoard/SortServicesByCreatedAt/true`

**Query Parameters:**
- `ascending` (bool) - true for ascending, false for descending

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "createdAt": "2024-01-01T00:00:00Z",
    ...
  }
]
```

---

## Email Credentials API

**⚠️ SECURITY NOTE:** Email credentials contain sensitive encrypted passwords. Use with caution.

### Add Email Credential
**POST** `/api/EmailCredentials/AddCredential`

**Request Body:**
```json
{
  "emailAddress": "admin@example.com",
  "password": "SecurePassword123!",
  "userName": "admin_user",
  "customerId": 1,
  "hostingPlatform": "Bluehost",
  "notes": "Main admin email"
}
```

**Response:** `200 OK`
```json
{
  "message": "Credential added successfully"
}
```

### Get Email Credential (Without Password)
**GET** `/api/EmailCredentials/{id}`

**Example:** `/api/EmailCredentials/1`

**Response:** `200 OK`
```json
{
  "id": 1,
  "customerId": 1,
  "emailAddress": "admin@example.com",
  "userName": "admin_user",
  "hostingPlatform": "Bluehost",
  "notes": "Main admin email",
  "createdAt": "2024-01-01T00:00:00Z",
  "lastUsedAt": "2024-01-02T00:00:00Z",
  "auditLog": "[{\"action\":\"Created\",\"by\":\"API\",\"date\":\"2024-01-01T00:00:00Z\"}]"
}
```

### Get Email Credential With Decrypted Password
**⚠️ USE WITH EXTREME CAUTION**

**GET** `/api/EmailCredentials/{id}/with-password`

**Example:** `/api/EmailCredentials/1/with-password`

**Response:** `200 OK`
```json
{
  "id": 1,
  "customerId": 1,
  "emailAddress": "admin@example.com",
  "decryptedPassword": "SecurePassword123!",
  "userName": "admin_user",
  "hostingPlatform": "Bluehost",
  "notes": "Main admin email",
  "createdAt": "2024-01-01T00:00:00Z",
  "lastUsedAt": "2024-01-02T00:00:00Z",
  "auditLog": "[{\"action\":\"Accessed\",\"by\":\"API\",\"date\":\"2024-01-02T00:00:00Z\"}]"
}
```

### Get All Email Credentials
**GET** `/api/EmailCredentials?includeDeleted=false`

**Query Parameters:**
- `includeDeleted` (bool, default: false) - Include soft-deleted credentials

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "customerId": 1,
    "emailAddress": "admin@example.com",
    "userName": "admin_user",
    ...
  }
]
```

### Update Email Credential
**PUT** `/api/EmailCredentials/UpdateCredential`

**Request Body:**
```json
{
  "id": 1,
  "userName": "updated_user",
  "password": "NewSecurePassword123!",
  "customerId": 1,
  "hostingPlatform": "AWS",
  "notes": "Updated notes"
}
```

**Note:** Only include fields you want to update. Omit fields to keep existing values.

**Response:** `200 OK`
```json
{
  "message": "Credential updated successfully"
}
```

### Delete Email Credential (Soft Delete)
**DELETE** `/api/EmailCredentials/{id}`

**Example:** `/api/EmailCredentials/1`

**Response:** `200 OK`
```json
{
  "message": "Credential soft deleted successfully"
}
```

---

## Error Responses

All endpoints may return the following error responses:

### 400 Bad Request
```json
{
  "error": "Error message describing what went wrong"
}
```

### 404 Not Found
```json
{
  "error": "Resource not found"
}
```

### 500 Internal Server Error
```json
{
  "error": "Internal server error message"
}
```

---

## Angular Service Example

Here's an example of how to create an Angular service to interact with the API:

```typescript
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'http://localhost:5174/api';
  private token: string | null = null;

  constructor(private http: HttpClient) {
    // Load token from localStorage
    this.token = localStorage.getItem('authToken');
  }

  setToken(token: string) {
    this.token = token;
    localStorage.setItem('authToken', token);
  }

  private getHeaders(): HttpHeaders {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    if (this.token) {
      headers = headers.set('Authorization', `Bearer ${this.token}`);
    }

    return headers;
  }

  // Auth Methods
  login(email: string, password: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/Auth/login`, { email, password });
  }

  register(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Auth/register`, data);
  }

  // Customer Methods
  getAllCustomers(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Customer/GetAllCustomers`, {
      headers: this.getHeaders()
    });
  }

  getCustomerById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/Customer/GetCustomerById/${id}`, {
      headers: this.getHeaders()
    });
  }

  addCustomer(customer: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Customer/AddCustomer`, customer, {
      headers: this.getHeaders()
    });
  }

  updateCustomer(customer: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/Customer/UpdateCustomer`, customer, {
      headers: this.getHeaders()
    });
  }

  deleteCustomer(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Customer/DeleteCustomer/${id}`, {
      headers: this.getHeaders()
    });
  }

  searchCustomers(query: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/Customer/SearchCustomers?query=${query}`, {
      headers: this.getHeaders()
    });
  }

  // Expense Methods
  getAllExpenses(page: number = 1, pageSize: number = 10): Observable<any> {
    return this.http.get(`${this.baseUrl}/Expense?pageNumper=${page}&pageSize=${pageSize}`, {
      headers: this.getHeaders()
    });
  }

  getExpenseById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/Expense/${id}`, {
      headers: this.getHeaders()
    });
  }

  addExpense(expense: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Expense`, expense, {
      headers: this.getHeaders()
    });
  }

  updateExpense(expense: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/Expense`, expense, {
      headers: this.getHeaders()
    });
  }

  deleteExpense(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Expense/${id}`, {
      headers: this.getHeaders()
    });
  }

  getTotalExpensesAmount(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Expense/count`, {
      headers: this.getHeaders()
    });
  }

  // Revenue Methods
  getTotalRevenuesAmount(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Revenue/TotalRevenues`, {
      headers: this.getHeaders()
    });
  }

  addRevenue(revenue: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Revenue/AddRevenue`, revenue, {
      headers: this.getHeaders()
    });
  }

  getRevenueById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/Revenue/GetRevenueById/${id}`, {
      headers: this.getHeaders()
    });
  }

  updateRevenue(revenue: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/Revenue/EditRevenue`, revenue, {
      headers: this.getHeaders()
    });
  }

  deleteRevenue(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Revenue/DeleteRevenue/${id}`, {
      headers: this.getHeaders()
    });
  }

  // Contact Message Methods
  addContactMessage(message: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/ContactMessage/AddContactMessage`, message);
  }

  getPagedContactMessages(page: number = 1, pageSize: number = 10): Observable<any> {
    return this.http.get(`${this.baseUrl}/ContactMessage/GetPagedContactMessages?page=${page}&pageSize=${pageSize}`, {
      headers: this.getHeaders()
    });
  }

  getContactMessageById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/ContactMessage/GetContactMessageById/${id}`, {
      headers: this.getHeaders()
    });
  }

  deleteContactMessage(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/ContactMessage/DeleteContactMessage/${id}`, {
      headers: this.getHeaders()
    });
  }

  markContactMessageAsRead(id: number): Observable<any> {
    return this.http.put(`${this.baseUrl}/ContactMessage/MarkAsRead/${id}`, {}, {
      headers: this.getHeaders()
    });
  }

  getContactMessagesByReadStatus(isRead: boolean): Observable<any> {
    return this.http.get(`${this.baseUrl}/ContactMessage/GetContactMessagesByReadStatus?isRead=${isRead}`, {
      headers: this.getHeaders()
    });
  }

  // Services on Main Methods
  getAllServicesOnMain(): Observable<any> {
    return this.http.get(`${this.baseUrl}/ServicesOnMain`);
  }

  getServiceOnMainById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/ServicesOnMain/${id}`);
  }

  addServiceOnMain(service: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/ServicesOnMain`, service, {
      headers: this.getHeaders()
    });
  }

  updateServiceOnMain(service: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/ServicesOnMain`, service, {
      headers: this.getHeaders()
    });
  }

  deleteServiceOnMain(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/ServicesOnMain/${id}`, {
      headers: this.getHeaders()
    });
  }

  // Services on Dashboard Methods
  getAllServicesOnDashboard(): Observable<any> {
    return this.http.get(`${this.baseUrl}/ServicesOnDashBoard/GetAllServicesOnDashBoard`, {
      headers: this.getHeaders()
    });
  }

  getServiceOnDashboardById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/ServicesOnDashBoard/GetServiceOnDashBoardById/${id}`, {
      headers: this.getHeaders()
    });
  }

  addServiceOnDashboard(service: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/ServicesOnDashBoard/AddServiceOnDashBoard`, service, {
      headers: this.getHeaders()
    });
  }

  updateServiceOnDashboard(service: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/ServicesOnDashBoard/UpdateServiceOnDashBoard`, service, {
      headers: this.getHeaders()
    });
  }

  deleteServiceOnDashboard(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/ServicesOnDashBoard/DeleteServiceOnDashBoard/${id}`, {
      headers: this.getHeaders()
    });
  }

  searchServicesByName(serviceName: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/ServicesOnDashBoard/SearchServicesByName/${serviceName}`, {
      headers: this.getHeaders()
    });
  }

  filterServicesByCMS(cms: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/ServicesOnDashBoard/FilterServicesByCMS/${cms}`, {
      headers: this.getHeaders()
    });
  }

  filterServicesByStatus(status: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/ServicesOnDashBoard/FilterServicesByStatus/${status}`, {
      headers: this.getHeaders()
    });
  }

  sortServicesByCreatedAt(ascending: boolean): Observable<any> {
    return this.http.get(`${this.baseUrl}/ServicesOnDashBoard/SortServicesByCreatedAt/${ascending}`, {
      headers: this.getHeaders()
    });
  }

  // Email Credentials Methods
  addEmailCredential(credential: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/EmailCredentials/AddCredential`, credential, {
      headers: this.getHeaders()
    });
  }

  getEmailCredential(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/EmailCredentials/${id}`, {
      headers: this.getHeaders()
    });
  }

  getEmailCredentialWithPassword(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/EmailCredentials/${id}/with-password`, {
      headers: this.getHeaders()
    });
  }

  getAllEmailCredentials(includeDeleted: boolean = false): Observable<any> {
    return this.http.get(`${this.baseUrl}/EmailCredentials?includeDeleted=${includeDeleted}`, {
      headers: this.getHeaders()
    });
  }

  updateEmailCredential(credential: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/EmailCredentials/UpdateCredential`, credential, {
      headers: this.getHeaders()
    });
  }

  deleteEmailCredential(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/EmailCredentials/${id}`, {
      headers: this.getHeaders()
    });
  }
}
```

---

## TypeScript Interfaces Example

```typescript
// Customer Interface
export interface Customer {
  id: number;
  fullName: string;
  email?: string;
  phone?: string;
  companyName?: string;
  country?: string;
  notes?: string;
  createdAt: string;
}

// Expense Interface
export interface Expense {
  id: number;
  title: string;
  amount: number;
  category?: string;
  date: string;
  notes?: string;
  isDeleted: boolean;
}

// Revenue Interface
export interface Revenue {
  id: number;
  customerId: number;
  serviceId: number;
  amount: number;
  paymentDate: string;
  paymentMethod?: string;
  notes?: string;
}

// Contact Message Interface
export interface ContactMessage {
  id: number;
  name: string;
  email: string;
  message: string;
  createdAt: string;
}

// Service on Main Interface
export interface ServiceOnMain {
  id: number;
  title: string;
  description: string;
  price: number;
  imageUrl?: string;
  category?: string;
  isActive: boolean;
  createdAt: string;
  isDeleted: boolean;
}

// Service on Dashboard Interface
export interface ServiceOnDashboard {
  id: number;
  customerId: number;
  serviceName: string;
  domain: string;
  userName: string;
  cms: WebsiteCMS;
  hostingServer?: string;
  status: WebsiteStatus;
  notes?: string;
  createdAt: string;
  isDeleted: boolean;
}

// Enums
export enum WebsiteCMS {
  WordPress = 0,
  DotNet = 1,
  Joomla = 2,
  Drupal = 3,
  Custom = 4
}

export enum WebsiteStatus {
  Active = 0,
  Suspended = 1,
  InProgress = 2
}

// Email Credential Interface
export interface EmailCredential {
  id: number;
  customerId?: number;
  emailAddress: string;
  decryptedPassword?: string; // Only available when explicitly requested
  userName: string;
  hostingPlatform?: string;
  notes?: string;
  createdAt: string;
  lastUsedAt?: string;
  auditLog?: string;
}
```

---

## Notes for Frontend Developers

1. **Authentication**: Store the JWT token in localStorage or a secure storage mechanism after login/register.

2. **Error Handling**: Always handle errors appropriately. Check for 401 Unauthorized responses to redirect to login.

3. **CORS**: The API is configured to allow all origins in development. Adjust CORS settings for production.

4. **Date Format**: All dates are in ISO 8601 format (UTC).

5. **Pagination**: Use pagination for large datasets (Expenses, Contact Messages).

6. **Email Credentials**: Be extremely careful when handling decrypted passwords. Never log them or store them in plain text.

7. **Enum Values**: Use the numeric enum values when sending CMS and Status values to the API.

8. **Soft Deletes**: Most entities support soft deletes. Deleted items won't appear in normal queries unless explicitly requested.

---

## Testing the API

You can test the API using:
- Swagger UI: `http://localhost:5174/swagger` (in development)
- Postman
- Angular HttpClient
- Any HTTP client

---

## Support

For API support or questions, contact the backend development team.

