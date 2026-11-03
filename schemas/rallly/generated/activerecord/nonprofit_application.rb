class NonprofitApplication < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }

  enum :status, { approved: "approved", rejected: "rejected", failed: "failed" }, validate: true

  validates :organization_name, presence: true
  validates :website, presence: true
  validates :email_domain, presence: true

  belongs_to :space, inverse_of: :nonprofit_applications
  belongs_to :user, inverse_of: :nonprofit_applications
end
