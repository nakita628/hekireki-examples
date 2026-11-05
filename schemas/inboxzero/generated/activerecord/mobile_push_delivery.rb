class MobilePushDelivery < ApplicationRecord
  self.table_name = "MobilePushDelivery"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :deduplicationKey, presence: true, uniqueness: { scope: :mobilePushTokenId }

  belongs_to :mobile_push_token, foreign_key: "mobilePushTokenId", inverse_of: :mobile_push_deliveries
end
