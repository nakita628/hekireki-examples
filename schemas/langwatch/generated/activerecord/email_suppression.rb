class EmailSuppression < ApplicationRecord
  self.table_name = "EmailSuppression"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :reason, default: "unsubscribe"
  alias_attribute :created_at, :createdAt

  validates :email, presence: true,
                    uniqueness: { scope: [ :projectId, :triggerId ], conditions: -> { where.not(triggerId: nil) } }

  belongs_to :project, foreign_key: "projectId", inverse_of: :email_suppressions
end
