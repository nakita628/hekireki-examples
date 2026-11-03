class OauthRefreshToken < ApplicationRecord
  self.table_name = "oauthRefreshToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :scopes, default: -> { [] }
  attribute :resources, default: -> { [] }
  attribute :requestedUserInfoClaims, default: -> { [] }
  alias_attribute :created_at, :createdAt

  validates :token, presence: true, uniqueness: true
  validates :expiresAt, presence: true

  belongs_to :client, class_name: "OauthClient", foreign_key: "clientId", primary_key: "clientId",
                      inverse_of: :refresh_tokens
  belongs_to :session, foreign_key: "sessionId", optional: true, inverse_of: :oauth_refresh_tokens
  belongs_to :user, foreign_key: "userId", inverse_of: :oauth_refresh_tokens
  has_many :access_tokens, class_name: "OauthAccessToken", foreign_key: "refreshId", inverse_of: :refresh,
                           dependent: :destroy
end
