class Donation < ApplicationRecord
  self.table_name = "Donation"

  alias_attribute :created_at, :createdAt

  validates :amount, presence: true
  validates :stripeCheckoutSessionId, presence: true, uniqueness: true

  belongs_to :user, foreign_key: "userId", inverse_of: :donations
end
