class Post < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :published, default: false

  validates :title, presence: true

  belongs_to :author, class_name: "User", foreign_key: "authorId", inverse_of: :post
end
