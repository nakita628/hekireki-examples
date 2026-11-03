class PayoutProblems < ApplicationRecord
  self.table_name = "PayoutProblems"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :status, presence: true
  validates :amount, presence: true

  belongs_to :order, class_name: "Orders", foreign_key: "orderId", inverse_of: :payout_problems
  belongs_to :post, foreign_key: "postId", optional: true, inverse_of: :payout_problems
  belongs_to :user, foreign_key: "userId", inverse_of: :payout_problems
end
