class Response < ApplicationRecord
  self.table_name = "Response"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :finished, default: false
  attribute :data, default: -> { JSON.parse("{}") }
  attribute :variables, default: -> { JSON.parse("{}") }
  attribute :ttc, default: -> { JSON.parse("{}") }
  attribute :meta, default: -> { JSON.parse("{}") }

  enum :outcome, { low_quality: "lowQuality" }, validate: { allow_nil: true }

  validates :singleUseId, uniqueness: { scope: :surveyId, allow_nil: true }
  validates :displayId, uniqueness: { allow_nil: true }

  belongs_to :survey, foreign_key: "surveyId", inverse_of: :responses
  belongs_to :contact, foreign_key: "contactId", optional: true, inverse_of: :responses
  belongs_to :display, foreign_key: "displayId", optional: true, inverse_of: :response
  has_many :tags, class_name: "TagsOnResponses", foreign_key: "responseId", inverse_of: :response, dependent: :destroy
  has_many :quota_links, class_name: "ResponseQuotaLink", foreign_key: "responseId", inverse_of: :response,
                         dependent: :destroy
  has_many :workflow_runs, foreign_key: "responseId", inverse_of: :response, dependent: :destroy
end
