class WebPushVapidKey < ApplicationRecord
  self.table_name = "WebPushVapidKey"

  attribute :id, default: "self"
  alias_attribute :created_at, :createdAt

  validates :publicKey, presence: true
  validates :privateKeyEncrypted, presence: true
end
