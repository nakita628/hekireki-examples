class ShareReport < ApplicationRecord
  attribute :public, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :id, presence: true, uniqueness: true
  validates :reportId, uniqueness: true

  belongs_to :report, foreign_key: "reportId", inverse_of: :share
  belongs_to :organization, foreign_key: "organizationId", inverse_of: :share_report
  belongs_to :project, foreign_key: "projectId", inverse_of: :share_reports
end
