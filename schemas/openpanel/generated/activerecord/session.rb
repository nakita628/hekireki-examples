class Session < ApplicationRecord
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :token, uniqueness: { allow_nil: true }
  validates :expiresAt, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :sessions
end
