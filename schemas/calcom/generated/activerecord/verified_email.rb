class VerifiedEmail < ApplicationRecord
  self.table_name = "VerifiedEmail"

  validates :email, presence: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :verified_emails
  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :verified_emails
end
