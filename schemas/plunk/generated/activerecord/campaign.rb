class Campaign < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :totalRecipients, default: 0
  attribute :sentCount, default: 0
  attribute :deliveredCount, default: 0
  attribute :openedCount, default: 0
  attribute :clickedCount, default: 0
  attribute :bouncedCount, default: 0
  attribute :complainedCount, default: 0
  attribute :unsubscribedCount, default: 0
  attribute :archived, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { draft: "DRAFT", scheduled: "SCHEDULED", sending: "SENDING", sent: "SENT", cancelled: "CANCELLED" },
       default: :draft, validate: true
  enum :type, { transactional: "TRANSACTIONAL", marketing: "MARKETING", headless: "HEADLESS" }, default: :marketing,
                                                                                                validate: true
  enum :audienceType, { all: "ALL", filtered: "FILTERED", segment: "SEGMENT" }, default: :all, prefix: true,
                                                                                validate: true

  validates :name, presence: true
  validates :subject, presence: true
  validates :body, presence: true
  validates :from, presence: true

  belongs_to :segment, foreign_key: "segmentId", optional: true, inverse_of: :campaigns
  belongs_to :project, foreign_key: "projectId", inverse_of: :campaigns
  has_many :emails, foreign_key: "campaignId", inverse_of: :campaign, dependent: :destroy
end
