class RestrictedToken < ApplicationRecord
  self.table_name = "RestrictedToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :mode, default: "live"
  attribute :source, default: "dashboard"
  attribute :subjectType, default: "user"
  attribute :rateLimit, default: 60
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :hashedKey, presence: true, uniqueness: true
  validates :partialKey, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :restricted_tokens
  belongs_to :team, foreign_key: "teamId", inverse_of: :restricted_tokens
  belongs_to :oauth_client, class_name: "OAuthClient", foreign_key: "clientId", optional: true, inverse_of: :tokens
end
