class PlatformOAuthClient < ApplicationRecord
  self.table_name = "PlatformOAuthClient"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :redirectUris, default: -> { [] }
  attribute :areEmailsEnabled, default: false
  attribute :areDefaultEventTypesEnabled, default: true
  attribute :areCalendarEventsEnabled, default: true
  alias_attribute :created_at, :createdAt

  validates :name, presence: true
  validates :secret, presence: true
  validates :permissions, presence: true

  belongs_to :organization, class_name: "Team", foreign_key: "organizationId", inverse_of: :platform_oauth_client
  has_many :teams, foreign_key: "createdByOAuthClientId", inverse_of: :created_by_oauth_client, dependent: :destroy
  has_many :access_tokens, foreign_key: "platformOAuthClientId", inverse_of: :client, dependent: :destroy
  has_many :refresh_token, foreign_key: "platformOAuthClientId", inverse_of: :client, dependent: :destroy
  has_many :authorization_tokens, class_name: "PlatformAuthorizationToken", foreign_key: "platformOAuthClientId",
                                  inverse_of: :client, dependent: :destroy
  has_many :webhook, foreign_key: "platformOAuthClientId", inverse_of: :platform_oauth_client, dependent: :destroy
  has_and_belongs_to_many :users, join_table: "_PlatformOAuthClientToUser", foreign_key: "A",
                                  association_foreign_key: "B"
end
