class Session < ApplicationRecord
  self.table_name = "session"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :expiresAt, presence: true
  validates :token, presence: true, uniqueness: true

  belongs_to :user, foreign_key: "userId", inverse_of: :sessions
end
