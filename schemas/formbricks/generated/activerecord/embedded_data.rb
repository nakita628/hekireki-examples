class EmbeddedData < ApplicationRecord
  self.table_name = "EmbeddedData"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :locked, default: false

  enum :source, { computed: "computed", ingested: "ingested", reserved: "reserved" }, validate: true
  enum :dataType, { string: "string", number: "number", boolean: "boolean", date: "date" }, default: :string,
                                                                                            validate: true

  validates :id, uniqueness: { scope: :workspaceId }
  validates :key, uniqueness: { scope: :workspaceId, allow_nil: true }
  validates :name, presence: true

  belongs_to :survey, foreign_key: [ :surveyId, :workspaceId ], primary_key: [ :id, :workspaceId ], optional: true,
                      inverse_of: :embedded_data
  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :embedded_data
  has_many :survey_links, class_name: "SurveyEmbeddedData", foreign_key: [ :embeddedDataId, :workspaceId ],
                          primary_key: [ :id, :workspaceId ], inverse_of: :embedded_data, dependent: :destroy
end
