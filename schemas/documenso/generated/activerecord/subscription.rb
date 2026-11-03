class Subscription < ApplicationRecord
  self.table_name = "Subscription"

  attribute :cancelAtPeriodEnd, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { active: "ACTIVE", past_due: "PAST_DUE", inactive: "INACTIVE" }, default: :inactive, validate: true

  validates :planId, presence: true, uniqueness: true
  validates :priceId, presence: true
  validates :customerId, presence: true
  validates :organisationId, uniqueness: true

  belongs_to :organisation, foreign_key: "organisationId", inverse_of: :subscription
end
