defmodule InboxZero.DocumentFiling do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          message_id: String.t(),
          attachment_id: String.t(),
          filename: String.t(),
          folder_id: String.t() | nil,
          folder_path: String.t(),
          file_id: String.t() | nil,
          reasoning: String.t() | nil,
          confidence: float() | nil,
          status: atom(),
          was_asked: boolean(),
          was_corrected: boolean(),
          original_path: String.t() | nil,
          corrected_at: DateTime.t() | nil,
          feedback_positive: boolean() | nil,
          feedback_at: DateTime.t() | nil,
          notification_message_id: String.t() | nil,
          notification_sent_at: DateTime.t() | nil,
          notification_batch_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          drive_connection: InboxZero.DriveConnection.t() | nil,
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "DocumentFiling" do
    field(:id, :string, primary_key: true)
    field(:message_id, :string, source: :messageId)
    field(:attachment_id, :string, source: :attachmentId)
    field(:filename, :string)
    field(:folder_id, :string, source: :folderId)
    field(:folder_path, :string, source: :folderPath)
    field(:file_id, :string, source: :fileId)
    field(:reasoning, :string)
    field(:confidence, :float)
    field(:status, Ecto.Enum, values: [:PROCESSING, :PENDING, :FILED, :REJECTED, :ERROR, :PREVIEW], default: :FILED)
    field(:was_asked, :boolean, default: false, source: :wasAsked)
    field(:was_corrected, :boolean, default: false, source: :wasCorrected)
    field(:original_path, :string, source: :originalPath)
    field(:corrected_at, InboxZero.PrismaDateTime, source: :correctedAt)
    field(:feedback_positive, :boolean, source: :feedbackPositive)
    field(:feedback_at, InboxZero.PrismaDateTime, source: :feedbackAt)
    field(:notification_message_id, :string, source: :notificationMessageId)
    field(:notification_sent_at, InboxZero.PrismaDateTime, source: :notificationSentAt)
    field(:notification_batch_id, :string, source: :notificationBatchId)
    field(:drive_connection_id, :string, source: :driveConnectionId)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:drive_connection, InboxZero.DriveConnection, foreign_key: :drive_connection_id, define_field: false, type: :string)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end