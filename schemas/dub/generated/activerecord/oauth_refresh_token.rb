class OAuthRefreshToken < ApplicationRecord
  self.table_name = "OAuthRefreshToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :hashedRefreshToken, presence: true, uniqueness: true
  validates :expiresAt, presence: true

  belongs_to :access_token, class_name: "RestrictedToken", foreign_key: "accessTokenId", inverse_of: :refresh_tokens
  belongs_to :installed_integration, foreign_key: "installationId", inverse_of: :refresh_tokens
end
