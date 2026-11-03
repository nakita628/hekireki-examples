class OrganizationOnboarding < ApplicationRecord
  self.table_name = "OrganizationOnboarding"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :isPlatform, default: false
  attribute :isDomainConfigured, default: false
  attribute :invitedMembers, default: -> { JSON.parse("[]") }
  attribute :teams, default: -> { JSON.parse("[]") }
  attribute :isComplete, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :billingPeriod, { monthly: "MONTHLY", annually: "ANNUALLY" }, validate: true

  validates :orgOwnerEmail, presence: true, uniqueness: true
  validates :organizationId, uniqueness: { allow_nil: true }
  validates :pricePerSeat, presence: true
  validates :seats, presence: true
  validates :name, presence: true
  validates :slug, presence: true
  validates :stripeCustomerId, uniqueness: { allow_nil: true }

  belongs_to :created_by, class_name: "User", foreign_key: "createdById", inverse_of: :created_organization_onboardings
  belongs_to :organization, class_name: "Team", foreign_key: "organizationId", optional: true,
                            inverse_of: :organization_onboarding
end
