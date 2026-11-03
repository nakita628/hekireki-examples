class Comment < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :content, presence: true
  validates :author_name, presence: true

  belongs_to :poll, inverse_of: :comments
  belongs_to :user, optional: true, inverse_of: :comments
end
