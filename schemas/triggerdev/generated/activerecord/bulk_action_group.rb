class BulkActionGroup < ApplicationRecord
  self.table_name = "BulkActionGroup"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :successCount, default: 0
  attribute :failureCount, default: 0
  attribute :totalCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { cancel: "CANCEL", replay: "REPLAY" }, validate: true
  enum :status, { pending: "PENDING", completed: "COMPLETED", aborted: "ABORTED" }, default: :pending, validate: true
  enum :completionNotification, { none: "NONE", email: "EMAIL" }, default: :none, prefix: true, validate: true

  validates :friendlyId, presence: true, uniqueness: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :bulk_action_groups
  belongs_to :environment, class_name: "RuntimeEnvironment", foreign_key: "environmentId", optional: true,
                           inverse_of: :bulk_action_group
  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :bulk_actions
end
