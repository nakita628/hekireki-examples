class Contact < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :subscribed, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :email, presence: true, uniqueness: { scope: :projectId }

  belongs_to :project, foreign_key: "projectId", inverse_of: :contacts
  has_many :emails, foreign_key: "contactId", inverse_of: :contact, dependent: :destroy
  has_many :workflow_executions, foreign_key: "contactId", inverse_of: :contact, dependent: :destroy
  has_many :events, foreign_key: "contactId", inverse_of: :contact, dependent: :destroy
  has_many :segment_memberships, foreign_key: "contactId", inverse_of: :contact, dependent: :destroy
  has_many :throttle_claims, foreign_key: "contactId", inverse_of: :contact, dependent: :destroy
end
