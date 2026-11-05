defmodule InboxZero.FilingFolder do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          folder_id: String.t(),
          folder_name: String.t(),
          folder_path: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          drive_connection: InboxZero.DriveConnection.t() | nil,
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "FilingFolder" do
    field(:id, :string, primary_key: true)
    field(:folder_id, :string, source: :folderId)
    field(:folder_name, :string, source: :folderName)
    field(:folder_path, :string, source: :folderPath)
    field(:drive_connection_id, :string, source: :driveConnectionId)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:drive_connection, InboxZero.DriveConnection, foreign_key: :drive_connection_id, define_field: false, type: :string)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end