class Dashboard < ApplicationRecord
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :dashboard
  belongs_to :project, foreign_key: "projectId", inverse_of: :dashboards
  has_one :share, class_name: "ShareDashboard", foreign_key: "dashboardId", inverse_of: :dashboard, dependent: :destroy
  has_many :reports, foreign_key: "dashboardId", inverse_of: :dashboard, dependent: :destroy
end
