class PromptVersion < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :labels, default: -> { [] }
  alias_attribute :created_at, :createdAt

  validates :version, presence: true, uniqueness: { scope: :promptId }
  validates :source, presence: true
  validates :contentHash, presence: true

  belongs_to :prompt, foreign_key: "promptId", inverse_of: :versions
  belongs_to :worker, class_name: "BackgroundWorker", foreign_key: "workerId", optional: true,
                      inverse_of: :prompt_versions
end
