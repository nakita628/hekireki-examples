class AccessToken < ApplicationRecord
  self.table_name = "AccessToken"

  attribute :revoked, default: false
  attribute :isSession, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :token, presence: true, uniqueness: true
  validates :expires, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :access_tokens
end
