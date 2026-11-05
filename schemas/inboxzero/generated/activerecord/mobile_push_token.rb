class MobilePushToken < ApplicationRecord
  self.table_name = "MobilePushToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :platform, { android: "android", ios: "ios" }, validate: true
  enum :tokenType, { expo: "expo", apns: "apns", fcm: "fcm" }, default: :expo, validate: true

  validates :token, presence: true, uniqueness: true

  belongs_to :user, foreign_key: "userId", inverse_of: :mobile_push_tokens
  has_many :mobile_push_deliveries, foreign_key: "mobilePushTokenId", inverse_of: :mobile_push_token,
                                    dependent: :destroy
end
