-- Insert Service FAQs

-- Service 1: Pet Spa & Grooming FAQs
INSERT INTO ServiceFAQs (Question, Answer, IsActive, DisplayOrder, CreatedAt, ServiceId)
VALUES 
(N'Spa & Grooming có an toàn cho thú cưng không?', N'Hoàn toàn an toàn. Chúng tôi sử dụng sản phẩm thiên nhiên, không gây kích ứng và kỹ thuật chuyên nghiệp. Đội ngũ được đào tạo chuyên sâu về xử lý thú cưng.', 1, 1, GETDATE(), 1),
(N'Dịch vụ Spa & Grooming mất bao lâu?', N'Tùy vào gói dịch vụ và kích thước thú cưng: Spa cơ bản 60-90 phút, Grooming đầy đủ 90-150 phút, các dịch vụ đặc biệt có thể lên đến 180 phút.', 1, 2, GETDATE(), 1),
(N'Tôi có thể chờ thú cưng tại shop không?', N'Tất nhiên! Chúng tôi có khu vực chờ thoải mái với WiFi miễn phí, đồ uống và có thể quan sát thú cưng qua camera.', 1, 3, GETDATE(), 1),
(N'Nên chuẩn bị gì trước khi đến Spa?', N'Mang theo sổ tiêm phòng, thông báo tình trạng sức khỏe đặc biệt nếu có. Không cho ăn quá no trước 2 tiếng và đưa thú cưng đi vệ sinh trước.', 1, 4, GETDATE(), 1),
(N'Có dịch vụ đưa đón tại nhà không?', N'Có, chúng tôi có dịch vụ đưa đón trong bán kính 10km với phí phù hợp. Vui lòng đặt lịch trước ít nhất 1 ngày.', 1, 5, GETDATE(), 1),

-- Service 2: Pet Hotel FAQs
(N'Pet Hotel có an toàn và thoải mái không?', N'Chúng tôi có camera giám sát 24/7, nhân viên chăm sóc chuyên nghiệp và phòng ở riêng biệt để đảm bảo an toàn. Môi trường được thiết kế thân thiện và thoải mái như ở nhà.', 1, 1, GETDATE(), 2),
(N'Thú cưng có được chơi và vận động không?', N'Có, chúng tôi có khu vực vui chơi trong nhà và ngoài trời, thời gian dạo chơi 3 lần/ngày và các hoạt động giải trí phù hợp với từng loại thú cưng.', 1, 2, GETDATE(), 2),
(N'Nếu thú cưng bị ốm trong lúc lưu trú?', N'Chúng tôi có bác sĩ thú y trực 24/7 và sẽ liên hệ ngay với chủ. Chi phí y tế sẽ được thông báo và xin phép trước khi thực hiện.', 1, 3, GETDATE(), 2),
(N'Tôi có nhận được thông tin về thú cưng hàng ngày không?', N'Có, chúng tôi gửi báo cáo hình ảnh, video và tình trạng sức khỏe qua app hoặc Zalo mỗi ngày vào lúc 6PM.', 1, 4, GETDATE(), 2),
(N'Cần chuẩn bị gì khi gửi thú cưng ở hotel?', N'Mang sổ tiêm phòng, đồ chơi quen thuộc, thức ăn (nếu có chế độ đặc biệt) và danh sách những điều cần lưu ý về thú cưng.', 1, 5, GETDATE(), 2),

-- Service 3: Pet Swimming FAQs
(N'Chó lần đầu bơi có an toàn không?', N'Hoàn toàn an toàn. Chúng tôi có áo phao chuyên dụng, huấn luyện viên kèm riêng và quy trình từng bước để chó làm quen với nước một cách tự nhiên.', 1, 1, GETDATE(), 3),
(N'Nước bể bơi có sạch và an toàn không?', N'Nước được lọc bằng hệ thống UV và ozone, kiểm tra pH hàng ngày. Tuyệt đối không dùng clo có hại cho da lông chó.', 1, 2, GETDATE(), 3),
(N'Chó bao nhiêu tuổi thì có thể bơi?', N'Chó từ 4 tháng tuổi trở lên, đã tiêm phòng đầy đủ. Tuổi lý tưởng là 6 tháng - 6 tuổi. Chó già cần kiểm tra sức khỏe trước.', 1, 3, GETDATE(), 3),
(N'Tại sao không có dịch vụ bơi cho mèo?', N'Mèo có cấu trúc lông và bản năng khác chó, thường sợ nước và có thể bị shock nhiệt. Chúng tôi ưu tiên an toàn tuyệt đối cho thú cưng.', 1, 4, GETDATE(), 3),
(N'Sau khi bơi có được chăm sóc gì?', N'Tắm sạch bằng dầu gội chuyên dụng, sấy khô hoàn toàn, kiểm tra tai mắt và có thể massage thư giãn nếu cần.', 1, 5, GETDATE(), 3),

-- Service 4: Pet Daycare FAQs
(N'Pet Daycare khác gì với Pet Hotel?', N'Daycare là trông giữ theo ngày (7AM-7PM), tập trung vào vui chơi, học tập. Pet Hotel là lưu trú qua đêm dài hạn. Daycare phù hợp khi bạn đi làm hoặc có việc bận trong ngày.', 1, 1, GETDATE(), 4),
(N'Thú cưng có được học kỹ năng gì không?', N'Có, chúng tôi dạy kỹ năng xã hội, vâng lời cơ bản, vệ sinh đúng chỗ và cách chơi đùa an toàn với bạn bè.', 1, 2, GETDATE(), 4),
(N'Nếu thú cưng nhút nhát, không hòa đồng?', N'Chúng tôi có khu vực riêng cho thú cưng nhút nhát, từ từ giúp chúng làm quen và hòa nhập thông qua các hoạt động phù hợp.', 1, 3, GETDATE(), 4),
(N'Có thể đưa thú cưng đến bất cứ lúc nào?', N'Nhận thú cưng từ 7AM-9AM và trả từ 5PM-7PM. Thời gian khác cần báo trước và có phụ phí.', 1, 4, GETDATE(), 4),
(N'Chi phí Daycare tính như thế nào?', N'Tính theo ngày hoặc gói tuần/tháng. Giá khác nhau tùy kích thước thú cưng và các dịch vụ thêm như ăn uống đặc biệt, huấn luyện riêng.', 1, 5, GETDATE(), 4),

-- Service 5: Pet Training FAQs
(N'Thú cưng bao nhiêu tuổi thì bắt đầu huấn luyện tốt nhất?', N'Chó từ 8-16 tuần tuổi, mèo từ 6-14 tuần tuổi là thời gian vàng. Tuy nhiên thú cưng lớn vẫn học được, chỉ cần thời gian và kiên trì hơn.', 1, 1, GETDATE(), 5),
(N'Huấn luyện chó và mèo có khác nhau không?', N'Rất khác! Chó học nhóm tốt, thích làm hài lòng chủ. Mèo học cá nhân, cần động lực bằng thưởng và sự tôn trọng tính độc lập của chúng.', 1, 2, GETDATE(), 5),
(N'Tôi có cần tham gia buổi huấn luyện không?', N'Có, ít nhất 50% buổi học cần có chủ để học cách giao tiếp và duy trì kết quả tại nhà. Chúng tôi cũng hướng dẫn chủ cách thực hành hiệu quả.', 1, 3, GETDATE(), 5),
(N'Thú cưng rất bướng bỉnh có huấn luyện được không?', N'Được! Thường thú cưng ''bướng bỉnh'' chỉ là chưa hiểu hoặc chưa có động lực đúng. Chúng tôi tìm ra phương pháp phù hợp với tính cách từng cá thể.', 1, 4, GETDATE(), 5),
(N'Sau khóa học có hỗ trợ gì thêm không?', N'Có tư vấn miễn phí trong 30 ngày, video hướng dẫn thực hành và có thể tham gia lớp ôn tập với giá ưu đãi.', 1, 5, GETDATE(), 5); 