class Template < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { transactional: "TRANSACTIONAL", marketing: "MARKETING", headless: "HEADLESS" }, default: :marketing,
                                                                                                validate: true

  validates :name, presence: true
  validates :subject, presence: true
  validates :body, presence: true
  validates :from, presence: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :templates
  has_many :emails, foreign_key: "templateId", inverse_of: :template, dependent: :nullify
  has_many :workflow_steps, foreign_key: "templateId", inverse_of: :template, dependent: :nullify
end
