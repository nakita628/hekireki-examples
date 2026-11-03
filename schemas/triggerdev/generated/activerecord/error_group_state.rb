class ErrorGroupState < ApplicationRecord
  self.table_name = "ErrorGroupState"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { unresolved: "UNRESOLVED", resolved: "RESOLVED", ignored: "IGNORED" }, default: :unresolved,
                                                                                        validate: true

  validates :taskIdentifier, presence: true,
                             uniqueness: { scope: [ :environmentId, :errorFingerprint ], conditions: -> { where.not(environmentId: nil) } }
  validates :errorFingerprint, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :error_group_states
  belongs_to :project, foreign_key: "projectId", inverse_of: :error_group_states
  belongs_to :environment, class_name: "RuntimeEnvironment", foreign_key: "environmentId", optional: true,
                           inverse_of: :error_group_states
end
