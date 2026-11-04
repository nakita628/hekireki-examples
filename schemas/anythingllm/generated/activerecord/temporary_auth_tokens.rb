class TemporaryAuthTokens < ApplicationRecord
  attribute :expiresAt, PrismaDateTime.new
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :token, presence: true, uniqueness: true
  validates :expiresAt, presence: true

  belongs_to :user, class_name: "Users", foreign_key: "userId", inverse_of: :temporary_auth_tokens
end
