class SurveyQuota < ApplicationRecord
  self.table_name = "SurveyQuota"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :logic, default: -> { JSON.parse("{}") }
  attribute :countPartialSubmissions, default: false

  enum :action, { end_survey: "endSurvey", continue_survey: "continueSurvey" }, validate: true

  validates :name, presence: true, uniqueness: { scope: :surveyId }
  validates :limit, presence: true

  belongs_to :survey, foreign_key: "surveyId", inverse_of: :quotas
  has_many :quota_links, class_name: "ResponseQuotaLink", foreign_key: "quotaId", inverse_of: :quota,
                         dependent: :destroy
end
