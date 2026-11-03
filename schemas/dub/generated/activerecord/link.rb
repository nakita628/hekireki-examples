class Link < ApplicationRecord
  self.table_name = "Link"

  attribute :archived, default: false
  attribute :trackConversion, default: false
  attribute :proxy, default: false
  attribute :rewrite, default: false
  attribute :doIndex, default: false
  attribute :publicStats, default: false
  attribute :clicks, default: 0
  attribute :leads, default: 0
  attribute :conversions, default: 0
  attribute :sales, default: 0
  attribute :saleAmount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :key, presence: true, uniqueness: { scope: :domain }
  validates :url, presence: true
  validates :shortLink, presence: true, length: { maximum: 400 }, uniqueness: true
  validates :description, length: { maximum: 280 }
  validates :utm_source, length: { maximum: 255 }
  validates :utm_medium, length: { maximum: 255 }
  validates :utm_campaign, length: { maximum: 255 }
  validates :utm_term, length: { maximum: 255 }
  validates :utm_content, length: { maximum: 255 }
  validates :externalId,
            uniqueness: { scope: :projectId, conditions: -> { where.not(projectId: nil) }, allow_nil: true }

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :links
  belongs_to :project, foreign_key: "projectId", optional: true, inverse_of: :links
  belongs_to :folder, foreign_key: "folderId", optional: true, inverse_of: :links
  belongs_to :short_domain, class_name: "Domain", foreign_key: "domain", primary_key: "slug", inverse_of: :links
  has_one :dashboard, foreign_key: "linkId", inverse_of: :link, dependent: :destroy
  has_many :tags, class_name: "LinkTag", foreign_key: "linkId", inverse_of: :link, dependent: :destroy
  has_many :webhooks, class_name: "LinkWebhook", foreign_key: "linkId", inverse_of: :link, dependent: :destroy
  has_many :customers, foreign_key: "linkId", inverse_of: :link, dependent: :nullify
end
