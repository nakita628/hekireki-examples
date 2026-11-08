class Email < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :opens, default: 0
  attribute :clicks, default: 0
  attribute :simulated, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :sourceType, { transactional: "TRANSACTIONAL", campaign: "CAMPAIGN", workflow: "WORKFLOW", inbound: "INBOUND" },
       validate: true
  enum :status, { scheduled: "SCHEDULED", cancelled: "CANCELLED", pending: "PENDING", sending: "SENDING", sent: "SENT", delivered: "DELIVERED", received: "RECEIVED", opened: "OPENED", clicked: "CLICKED", bounced: "BOUNCED", complained: "COMPLAINED", failed: "FAILED" },
       default: :pending, validate: true

  validates :subject, presence: true
  validates :body, presence: true
  validates :from, presence: true
  validates :messageId, uniqueness: { allow_nil: true }

  belongs_to :contact, foreign_key: "contactId", inverse_of: :emails
  belongs_to :template, foreign_key: "templateId", optional: true, inverse_of: :emails
  belongs_to :campaign, foreign_key: "campaignId", optional: true, inverse_of: :emails
  belongs_to :workflow_execution, foreign_key: "workflowExecutionId", optional: true, inverse_of: :emails
  belongs_to :workflow_step_execution, foreign_key: "workflowStepExecutionId", optional: true, inverse_of: :emails
  belongs_to :project, foreign_key: "projectId", inverse_of: :emails
  has_many :events, foreign_key: "emailId", inverse_of: :email, dependent: :destroy
end
