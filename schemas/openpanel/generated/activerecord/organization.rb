class Organization < ApplicationRecord
  attribute :onboarding, default: "completed"
  attribute :subscriptionPeriodEventsCount, default: 0
  attribute :subscriptionPeriodEventsLimit, default: 0
  attribute :subscriptionPauseAtPeriodEnd, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :slug, uniqueness: { allow_nil: true }

  belongs_to :created_by, class_name: "User", foreign_key: "createdByUserId", optional: true,
                          inverse_of: :created_organizations
  belongs_to :subscription_created_by, class_name: "User", foreign_key: "subscriptionCreatedByUserId", optional: true,
                                       inverse_of: :subscriptions
  has_many :projects, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :members, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :client, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :dashboard, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :share_overview, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :share_dashboard, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :share_report, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :share_widget, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :integrations, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :invites, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :conversations, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
end
