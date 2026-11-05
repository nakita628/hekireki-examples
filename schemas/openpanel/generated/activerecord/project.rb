class Project < ApplicationRecord
  attribute :eventsCount, default: 0
  attribute :cors, default: -> { [] }
  attribute :crossDomain, default: false
  attribute :allowUnsafeRevenueTracking, default: false
  attribute :aiStoreContent, default: true
  attribute :filters, default: -> { JSON.parse("[]") }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :projects
  has_one :share, class_name: "ShareOverview", foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_one :gsc_connection, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :clients, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :reports, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :dashboards, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :share_dashboards, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :share_reports, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :share_widgets, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :meta, class_name: "EventMeta", foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :custom_events, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :references, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :member_projects, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :notification_rules, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :notifications, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :imports, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :cohorts, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :export_watermarks, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :integrations, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :conversations, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
end
