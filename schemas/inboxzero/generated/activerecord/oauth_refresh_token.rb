class OauthRefreshToken < ApplicationRecord
  self.table_name = "OauthRefreshToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :resources, default: -> { [] }
  attribute :requestedUserInfoClaims, default: -> { [] }
  attribute :scopes, default: -> { [] }
  alias_attribute :created_at, :createdAt

  validates :token, presence: true, uniqueness: true
  validates :token, uniqueness: true
  validates :expiresAt, presence: true

  belongs_to :client, class_name: "OauthClient", foreign_key: "clientId", primary_key: "clientId",
                      inverse_of: :oauthrefreshtokens
  belongs_to :session, foreign_key: "sessionId", optional: true, inverse_of: :oauth_refresh_token_records
  belongs_to :user, foreign_key: "userId", inverse_of: :oauth_refresh_token_records
  has_many :oauthaccesstokens, class_name: "OauthAccessToken", foreign_key: "refreshId", inverse_of: :refresh,
                               dependent: :destroy
end
