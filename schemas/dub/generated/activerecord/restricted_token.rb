class RestrictedToken < ApplicationRecord
  self.table_name = "RestrictedToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :hashedKey, presence: true, uniqueness: true
  validates :partialKey, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :restricted_tokens
  belongs_to :project, foreign_key: "projectId", inverse_of: :restricted_tokens
  belongs_to :installed_integration, foreign_key: "installationId", optional: true, inverse_of: :access_tokens
  has_many :refresh_tokens, class_name: "OAuthRefreshToken", foreign_key: "accessTokenId", inverse_of: :access_token,
                            dependent: :destroy
end
