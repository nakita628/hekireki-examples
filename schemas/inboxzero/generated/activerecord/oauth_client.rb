class OauthClient < ApplicationRecord
  self.table_name = "OauthClient"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :disabled, default: false
  attribute :scopes, default: -> { [] }
  attribute :clientCredentialsScopes, default: -> { [] }
  attribute :contacts, default: -> { [] }
  attribute :redirectUris, default: -> { [] }
  attribute :postLogoutRedirectUris, default: -> { [] }
  attribute :grantTypes, default: -> { [] }
  attribute :responseTypes, default: -> { [] }
  attribute :dpopBoundAccessTokens, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :clientId, presence: true, uniqueness: true
  validates :clientId, uniqueness: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :oauth_client_records
  has_many :oauth_client_resources, foreign_key: "clientId", primary_key: "clientId", inverse_of: :client,
                                    dependent: :destroy
  has_many :oauthrefreshtokens, class_name: "OauthRefreshToken", foreign_key: "clientId", primary_key: "clientId",
                                inverse_of: :client, dependent: :destroy
  has_many :oauthaccesstokens, class_name: "OauthAccessToken", foreign_key: "clientId", primary_key: "clientId",
                               inverse_of: :client, dependent: :destroy
  has_many :oauthconsents, class_name: "OauthConsent", foreign_key: "clientId", primary_key: "clientId",
                           inverse_of: :client, dependent: :destroy
end
