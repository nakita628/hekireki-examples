class ScimRequestLog < ApplicationRecord
  self.table_name = "ScimRequestLog"

  attribute :id, default: -> { Nanoid.generate }

  validates :method, presence: true
  validates :resource, presence: true
  validates :status, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :scim_request_log
end
