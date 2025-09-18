# BindingList vs List

- List<T> (Danh sách thông thường): 
    - Không phù hợp cho "two-way data binding" (ràng buộc dữ liệu hai chiều) trong WinForms.
    - Khi bạn thay đổi danh sách này (thêm, xóa phần tử), giao diện người dùng **(UI)** như DataGridView sẽ **không tự động cập nhật.**   
    - Ngược lại, nếu UI thay đổi, danh sách cũng **không được cập nhật.**  

- BindingList<T>: 
    - Được thiết kế đặc biệt cho data binding. 
    - Nó triển khai các sự kiện đặc biệt như `ListChanged` và `AddingNew`. 
    - Những sự kiện này thông báo cho UI biết khi nào danh sách thay đổi, từ đó UI tự động cập nhật. 
    - Và ngược lại, thay đổi từ UI cũng được cập nhật vào danh sách.

## Import 
```cs
 public class BindingList<T> :
 System.Collections.ObjectModel.Collection<T>,
 System.ComponentModel.IBindingList,
 System.ComponentModel.ICancelAddNew,
 System.ComponentModel.IRaiseItemChangedEvents
```

## Kiểu tham số
Inheritance: Object → Collection<T> → BindingList<T>
Implements: ICollection, IEnumerable, IList, IBindingList, ICancelAddNew, IRaiseItemChangedEvents 

