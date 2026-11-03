class Orders < ApplicationRecord
  self.table_name = "Orders"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", accepted: "ACCEPTED", canceled: "CANCELED", completed: "COMPLETED" },
       validate: true

  belongs_to :buyer, class_name: "User", foreign_key: "buyerId", inverse_of: :order_buyer
  belongs_to :message_group, class_name: "MessagesGroup", foreign_key: "messageGroupId", inverse_of: :orders
  belongs_to :seller, class_name: "User", foreign_key: "sellerId", inverse_of: :order_seller
  has_many :orders_items, class_name: "OrderItems", foreign_key: "orderId", inverse_of: :order,
                          dependent: :restrict_with_error
  has_many :payout_problems, class_name: "PayoutProblems", foreign_key: "orderId", inverse_of: :order,
                             dependent: :restrict_with_error
  has_many :posts, foreign_key: "submittedForOrderId", inverse_of: :submitted_for_order, dependent: :nullify
end
