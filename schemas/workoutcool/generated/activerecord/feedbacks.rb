class Feedbacks < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :review, presence: true
  validates :message, presence: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :feedbacks
end
