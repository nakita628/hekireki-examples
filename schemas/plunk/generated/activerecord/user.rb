class User < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :emailVerified, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { password: "PASSWORD", google_oauth: "GOOGLE_OAUTH", github_oauth: "GITHUB_OAUTH" }, validate: true

  validates :email, presence: true, uniqueness: true

  has_many :memberships, foreign_key: "userId", inverse_of: :user, dependent: :restrict_with_error
end
