class SecondaryEmail < ApplicationRecord
  self.table_name = "SecondaryEmail"

  validates :email, presence: true, uniqueness: true
  validates :email, uniqueness: true
  validates :email, uniqueness: { scope: :userId }

  belongs_to :user, foreign_key: "userId", inverse_of: :secondary_emails
  has_many :verification_tokens, foreign_key: "secondaryEmailId", inverse_of: :secondary_email, dependent: :nullify
  has_many :event_types, foreign_key: "secondaryEmailId", inverse_of: :secondary_email, dependent: :destroy
end
