class OAuthAccessToken < ApplicationRecord
  self.table_name = "OAuthAccessToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :hash, presence: true
  validates :expiresAt, presence: true

  belongs_to :refresh_token, class_name: "UserApiToken", foreign_key: "refreshTokenId", inverse_of: :access_tokens
end
