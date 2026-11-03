class VerifiedNumber < ApplicationRecord
  self.table_name = "VerifiedNumber"

  validates :phoneNumber, presence: true

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :verified_numbers
  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :verified_numbers
end
