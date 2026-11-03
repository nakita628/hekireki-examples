class PlatformAuthorizationToken < ApplicationRecord
  self.table_name = "PlatformAuthorizationToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :userId, uniqueness: { scope: :platformOAuthClientId }

  belongs_to :owner, class_name: "User", foreign_key: "userId", inverse_of: :platform_authorization_token
  belongs_to :client, class_name: "PlatformOAuthClient", foreign_key: "platformOAuthClientId",
                      inverse_of: :authorization_tokens
end
