class TeamEmailVerification < ApplicationRecord
  self.table_name = "TeamEmailVerification"
  self.primary_key = "teamId"

  attribute :completed, default: false
  alias_attribute :created_at, :createdAt

  validates :name, presence: true
  validates :email, presence: true
  validates :token, presence: true, uniqueness: true
  validates :expiresAt, presence: true

  belongs_to :team, foreign_key: "teamId", inverse_of: :email_verification
end
