class DataPrivacyPolicy < ApplicationRecord
  self.table_name = "DataPrivacyPolicy"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :personalOnly, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :scopeType, { organization: "ORGANIZATION", department: "DEPARTMENT", team: "TEAM", project: "PROJECT" },
       validate: true

  validates :organizationId, presence: true
  validates :scopeId, presence: true, uniqueness: { scope: [ :scopeType, :personalOnly ] }
  validates :config, exclusion: { in: [ nil ] }
end
