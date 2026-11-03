class OrderItems < ApplicationRecord
  self.table_name = "OrderItems"

  attribute :id, default: -> { SecureRandom.uuid }

  validates :quantity, presence: true
  validates :price, presence: true

  belongs_to :integration, foreign_key: "integrationId", inverse_of: :order_items
  belongs_to :order, class_name: "Orders", foreign_key: "orderId", inverse_of: :orders_items
end
