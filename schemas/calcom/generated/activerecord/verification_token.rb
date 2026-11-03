class VerificationToken < ApplicationRecord
  self.table_name = "VerificationToken"

  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :identifier, presence: true, uniqueness: { scope: :token }
  validates :token, presence: true, uniqueness: true
  validates :expires, presence: true

  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :invite_tokens
  belongs_to :secondary_email, foreign_key: "secondaryEmailId", optional: true, inverse_of: :verification_tokens
end
