defmodule LangWatch.BugReport do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          source: String.t(),
          kind: String.t(),
          title: String.t(),
          summary: String.t() | nil,
          session_data: String.t() | nil,
          session_truncated: boolean(),
          agent: String.t() | nil,
          contact_email: String.t() | nil,
          cli_version: String.t() | nil,
          linked_project_id: String.t() | nil,
          metadata: map() | nil,
          inserted_at: DateTime.t()
        }

  schema "BugReport" do
    field(:id, :string, primary_key: true)
    field(:source, :string)
    field(:kind, :string)
    field(:title, :string)
    field(:summary, :string)
    field(:session_data, :string, source: :sessionData)
    field(:session_truncated, :boolean, default: false, source: :sessionTruncated)
    field(:agent, :string)
    field(:contact_email, :string, source: :contactEmail)
    field(:cli_version, :string, source: :cliVersion)
    field(:linked_project_id, :string, source: :linkedProjectId)
    field(:metadata, :map)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end